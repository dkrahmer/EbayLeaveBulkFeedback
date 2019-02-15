using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace EbayLeaveBulkFeedback
{
	public partial class MainForm : Form
	{
		private DataManager _dataManager;
		private Thread _leaveFeedbackThread;

		private RawEntryDialog _rawEntryDialog;
		public RawEntryDialog RawEntryDialog
		{
			get
			{
				if (_rawEntryDialog == null)
				{
					_rawEntryDialog = new RawEntryDialog(_dataManager)
					{
						AddListingIdAction = AddListingId,
						ListingIdsAddedAction = UpdateFeedbackListView
					};
				}

				return _rawEntryDialog;
			}
		}

		private void UpdateFeedbackListView()
		{
			_dataManager.UpdateFeedbackListViewAsync();
		}

		private void AddListingId(string listingId)
		{
			_dataManager.AddListingId(listingId);
		}

		private void RemoveListingId(string listingId)
		{
			_dataManager.RemoveListingId(listingId);
		}

		private ConfigDialog _configDialog;
		public ConfigDialog ConfigDialog
		{
			get
			{
				if (_configDialog == null || _configDialog.IsDisposed)
				{
					_configDialog = new ConfigDialog(_dataManager);
				}

				return _configDialog;
			}
		}

		public MainForm(DataManager dataManager)
		{
			InitializeComponent();
			_dataManager = dataManager;
			_dataManager.MainForm = this;
			_dataManager.FeedbackListViewChanged = UpdateItemCount;
			_dataManager.FeedbackListView = feedbackListView;
			InitListingPicker();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			ResetGui();
			//_dataManager.UpdateFeedbackListViewAsync();
			_dataManager.InitPickListView();
		}

		private void ResetGui()
		{
			Invoke((MethodInvoker)(() =>
			{
				EnableDisableAll(true);
				GeneralStatusUpdate("Ready", 0);
			}));
		}

		private void textBoxRawData_TextChanged(object sender, EventArgs e)
		{
			_dataManager.UpdateFeedbackListViewAsync();
		}

		private void buttonLeaveFeedback_Click(object sender, EventArgs e)
		{
			_leaveFeedbackThread = _dataManager.LeaveFeedbacksAsync(() => { EnableDisableAll(false); },
				ResetGui,
				GeneralStatusUpdate);
		}

		private void EnableDisableAll(bool enabled)
		{
			Invoke((MethodInvoker)(() =>
			{
				//textBoxRawData.Enabled = enabled;
				buttonLeaveFeedback.Enabled = enabled;
				buttonStop.Enabled = !enabled;
			}));
		}

		private void GeneralStatusUpdate(string status, int? percentComplete)
		{
			Invoke((MethodInvoker)(() =>
			{
				if (status != null)
					((ToolStripStatusLabel)(statusStrip.Items["toolStripStatusLabel"])).Text = status;

				if (percentComplete.HasValue)
					((ToolStripProgressBar)(statusStrip.Items["toolStripProgressBar"])).Value = percentComplete.Value;
			}));
		}
		/*
		private void FeedbackUpdate(string itemId, FeedbackUpdates updates)
		{
			_dataManager.FeedbackUpdate(itemId, updates);
		}
		*/
		private void buttonStop_Click(object sender, EventArgs e)
		{
			if (_leaveFeedbackThread != null && _leaveFeedbackThread.IsAlive)
			{
				_leaveFeedbackThread.Abort();
			}

			ResetGui();
		}

		private void buttonClearCompleted_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem listViewItem in feedbackListView.Items)
			{
				string status = listViewItem.SubItems[0].Text;
				if (status == "Done" || status == "Ignore")
					Invoke((MethodInvoker)(() => { feedbackListView.Items.Remove(listViewItem); }));
			}
		}


		private void UpdateItemCount()
		{
			toolStripItemCount.Text = "Items: " + feedbackListView.Items.Count.ToString();
		}

		private void buttonIgnoreListed_Click(object sender, EventArgs e)
		{
			var updates = new FeedbackUpdates()
			{
				Status = "Ignore"
			};

			foreach (ListViewItem item in feedbackListView.Items)
			{
				if (!string.IsNullOrEmpty(item.SubItems[0].Text))
					continue;   // don't update if it already has a status
				_dataManager.FeedbackUpdate(item.SubItems[1].Text, updates);
			}
		}

		private void buttonConfig_Click(object sender, EventArgs e)
		{
			ConfigDialog.Show();
			ConfigDialog.BringToFront();
		}

		private void btnRawEntry_Click(object sender, EventArgs e)
		{
			ShowRawEntryDialog();
		}

		private void ShowRawEntryDialog()
		{
			// RawEntryDialog.WindowState = FormWindowState.Maximized;
			RawEntryDialog.Show();
			RawEntryDialog.BringToFront();
		}

		// Listing Picker...
		//public Action<string> PickItemsAction { get; set; }
		private Thread _handlePressEnterAsyncThread;
		private const int PICK_SUBITEM_ITEM_ID = 2;
		private const int FEEDBACK_SUBITEM_ITEM_ID = 1;

		public void InitListingPicker()
		{
			//InitializeComponent();
			pickListView.LargeImageList = new ImageList()
			{
				ImageSize = new Size(140, 140),
				ColorDepth = ColorDepth.Depth32Bit
			};
			pickListView.ListViewItemSorter = new ListViewItemComparer(1);  // date
			pickListView.Sort();
			this.pickListView.Scroll += new ScrollEventHandler(listViewItems_Scroll);

			_dataManager.PickListViewChanged = UpdateItemCount;
			_dataManager.PickListView = pickListView;
			_dataManager.AddPickListViewImage = AddPickListViewImage;
			_dataManager.PickDialog = this;
		}

		/*
		private void ItemPickDialog_Load(object sender, EventArgs e)
		{
			_dataManager.InitPickListView();
		}

		private void ItemListDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason != CloseReason.UserClosing)
				return; // Do not close if the user hit the close button

			Hide();
			e.Cancel = true;
		}
		*/

		private void listViewItems_DoubleClick(object sender, EventArgs e)
		{
			_dataManager.ProcessSelectedPickListItems(AddListingId);
			_dataManager.UpdateFeedbackListViewAsync();
		}

		private void listViewItems_Scroll(object sender, ScrollEventArgs e)
		{
			pickListView.SelectedItems.Clear(); // prevent the scrolling from jumping around
			if (pickListView.FocusedItem != null)
				pickListView.FocusedItem.Focused = false;   // prevent the scrolling from jumping around
		}

		private bool UpdateItemCount(bool resetScroll)
		{
			if (resetScroll && pickListView.Items.Count > 0)
				pickListView.EnsureVisible(0);
			toolStripItemCount.Text = "Items: " + pickListView.Items.Count.ToString();

			return true;
		}

		private int AddPickListViewImage(Bitmap image)
		{
			int imageIndex = 0;

			Invoke((MethodInvoker)(() =>
			{
				pickListView.LargeImageList.Images.Add(image);
				imageIndex = pickListView.LargeImageList.Images.Count - 1;
			}));

			return imageIndex;
		}

		private void buttonSearch_Click(object sender, EventArgs e)
		{
			_dataManager.SearchString = textBoxSearch.Text;
		}

		private void textBoxSearch_TextChanged(object sender, EventArgs e)
		{
			_dataManager.SearchString = textBoxSearch.Text;
		}

		private void ItemPickDialog_Shown(object sender, EventArgs e)
		{
			textBoxSearch.Focus();
		}

		private void pickListView_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (int)Keys.Enter)
			{
				_dataManager.ProcessSelectedPickListItems(AddListingId);
				_dataManager.UpdateFeedbackListViewAsync();
				e.Handled = true;
				return;
			}

			textBoxSearch.Focus();
			SendKeys.Send(e.KeyChar.ToString());
			e.Handled = true;
		}

		private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyValue == (int)Keys.Down
				|| e.KeyValue == (int)Keys.Up
				|| e.KeyValue == (int)Keys.Enter)
			{
				if (e.KeyValue == (int)Keys.Enter)
				{
					if (_handlePressEnterAsyncThread != null && _handlePressEnterAsyncThread.IsAlive)
					{
						if (_handlePressEnterAsyncThread.ThreadState == ThreadState.WaitSleepJoin)
							try { _handlePressEnterAsyncThread.Interrupt(); } catch { }

						try { _handlePressEnterAsyncThread.Abort(); } catch { }
					}
					_handlePressEnterAsyncThread = new Thread(new ThreadStart(HandlePressEnter)) { IsBackground = true };
					_handlePressEnterAsyncThread.Start();
				}

				e.Handled = true;
			}
			else if (e.KeyCode == Keys.F6)
			{
				textBoxSearch.Focus();
				textBoxSearch.SelectAll();
				e.Handled = true;
			}
		}

		private void HandlePressEnter()
		{
			while (_dataManager.IsUpdateListViewAsyncThreadActive)
			{
				// Hack: Wait for the search thread to complete
				Thread.Sleep(10);
			}

			pickListView.Invoke((MethodInvoker)(() =>
			{
				pickListView.SelectedItems.Clear(); // prevent the scrolling from jumping around
				if (pickListView.FocusedItem != null)
					pickListView.FocusedItem.Focused = false;   // prevent the scrolling from jumping around

				if (pickListView.Items.Count > 0)
				{
					foreach (ListViewItem item in pickListView.Items)
					{
						item.Selected = true;
						item.Focused = true;
						pickListView.EnsureVisible(0);
					}

					_dataManager.ProcessSelectedPickListItems(AddListingId);
					_dataManager.UpdateFeedbackListViewAsync();
				}
				textBoxSearch.Focus();
				textBoxSearch.SelectAll();
			}));
		}

		private void buttonRefresh_Click(object sender, EventArgs e)
		{
			_dataManager.InitPickListView();
		}

		private void pickListView_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F6)
			{
				textBoxSearch.Focus();
				textBoxSearch.SelectAll();
				e.Handled = true;
			}
		}

		private void removeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem listItem in feedbackListView.SelectedItems)
			{
				string itemId = listItem.SubItems[FEEDBACK_SUBITEM_ITEM_ID].Text;
				RemoveListingId(itemId);
			}
			_dataManager.UpdateFeedbackListViewAsync();
		}

		private void ignoreToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem listItem in pickListView.SelectedItems)
			{
				string itemId = listItem.SubItems[PICK_SUBITEM_ITEM_ID].Text;

			}
		}

		private void addToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem listItem in pickListView.SelectedItems)
			{
				string itemId = listItem.SubItems[PICK_SUBITEM_ITEM_ID].Text;

				AddListingId(itemId);
			}
			_dataManager.UpdateFeedbackListViewAsync();
		}

		private void viewInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem listItem in pickListView.SelectedItems)
			{
				string itemId = listItem.SubItems[PICK_SUBITEM_ITEM_ID].Text;

				VIewInBrowser(itemId);
			}
		}

		private void viewInBrowserToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem listItem in feedbackListView.SelectedItems)
			{
				string itemId = listItem.SubItems[FEEDBACK_SUBITEM_ITEM_ID].Text;

				VIewInBrowser(itemId);
			}
		}

		private void VIewInBrowser(string itemId)
		{
			System.Diagnostics.Process.Start("http://www.ebay.com/itm/-/" + itemId + "?orig_cvip=true");
		}
	}
}
