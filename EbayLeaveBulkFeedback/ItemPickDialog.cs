using eBay.Service.Core.Soap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EbayLeaveBulkFeedback
{
	public partial class ItemPickDialog : Form
	{
		public Action<string> PickItemsAction { get; set; }
		private DataManager _dataManager;
		private Thread _handlePressEnterAsyncThread;
		const int PICK_SUBITEM_ITEM_ID = 2;

		public ItemPickDialog(DataManager dataManager)
		{
			InitializeComponent();
			pickListView.LargeImageList = new ImageList()
			{
				ImageSize = new Size(140, 140),
				ColorDepth = ColorDepth.Depth32Bit
			};
			pickListView.ListViewItemSorter = new ListViewItemComparer(1);  // date
			pickListView.Sort();
			this.pickListView.Scroll += new ScrollEventHandler(listViewItems_Scroll);
			_dataManager = dataManager;
			_dataManager.PickListViewChanged = UpdateItemCount;
			_dataManager.PickListView = pickListView;
			_dataManager.AddPickListViewImage = AddPickListViewImage;
			_dataManager.PickDialog = this;
		}

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

		private void listViewItems_DoubleClick(object sender, EventArgs e)
		{
			_dataManager.ProcessSelectedPickListItems(PickItemsAction);
		}

		private void pickListView_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				_dataManager.ProcessSelectedPickListItems(PickItemsAction);
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

		int AddPickListViewImage(Bitmap image)
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
				_dataManager.ProcessSelectedPickListItems(PickItemsAction);
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

					_dataManager.ProcessSelectedPickListItems(PickItemsAction);
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
			else if (e.KeyCode == Keys.F7)
			{
				foreach (ListViewItem listItem in pickListView.SelectedItems)
				{
					string itemId = listItem.SubItems[PICK_SUBITEM_ITEM_ID].Text;

					System.Diagnostics.Process.Start("http://www.ebay.com/itm/-/" + itemId + "?orig_cvip=true");
				}
				e.Handled = true;
			}

		}
	}
}
