using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace EbayLeaveBulkFeedback
{
	public partial class RawEntryDialog : Form
	{
		public Action<string> AddListingIdAction { get; set; }
		public Action ListingIdsAddedAction { get; set; }	
		private DataManager _dataManager;
		private Thread _rawListViewUpdaterThread;
		private string[] _listingIds;
		private string _textBoxRawDataDefaultText;

		private Action RawEntryListViewChanged { get; set; }

		public RawEntryDialog(DataManager dataManager)
		{
			InitializeComponent();
			_textBoxRawDataDefaultText = textBoxRawData.Text;
			_dataManager = dataManager;
			RawEntryListViewChanged = UpdateItemCount;
		}

		private void UpdateItemCount()
		{
			toolStripItemCount.Text = "Items: " + rawEntryListView.Items.Count.ToString();
		}

		private void RawEntryDialog_Load(object sender, EventArgs e)
		{
		}

		private void ItemListDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason != CloseReason.UserClosing)
				return; // Do not close if the user hit the close button

			Hide();
			e.Cancel = true;
		}

		private void RawEntryDialog_Shown(object sender, EventArgs e)
		{
			textBoxRawData.Focus();
		}


		private void textBoxRawData_TextChanged(object sender, EventArgs e)
		{
			UpdateRawEntryListViewAsync();
		}

		private void UpdateRawEntryListViewAsync(object isThread = null)
		{
			if (isThread == null)
			{
				if (_rawListViewUpdaterThread != null && _rawListViewUpdaterThread.IsAlive)
				{
					_rawListViewUpdaterThread.Abort();
				}

				_rawListViewUpdaterThread = new Thread(new ParameterizedThreadStart(UpdateRawEntryListViewAsync)) { IsBackground = true };
				_rawListViewUpdaterThread.Start(true);
				return;
			}

			try
			{
				string[] listingIds = null;
				Invoke((MethodInvoker)(() => { listingIds = Helpers.ExtractStringsByRegex(textBoxRawData.Text, "(\\d+)"); }));
				listingIds = listingIds.Where((itemId) => { return itemId.Length == 12; }).ToArray();
				var newListingListViewItems = new Dictionary<string, ListViewItem>();

				foreach (var listingId in listingIds)
				{
					if (newListingListViewItems.TryGetValue(listingId, out ListViewItem listViewItem))
						continue;   // no duplicates

					newListingListViewItems[listingId] = new ListViewItem(new string[] { listingId });
				}

				_listingIds = newListingListViewItems.Keys.ToArray();

				Invoke((MethodInvoker)(() => { rawEntryListView.BeginUpdate(); rawEntryListView.Items.Clear(); }));
				foreach (var newListingListViewItem in newListingListViewItems)
				{
					AuctionIdListAddItem(newListingListViewItem.Key, newListingListViewItem.Value, false);
				}
			}
			catch (Exception ex)
			{
				_dataManager.LogException(ex);
			}
			finally
			{
				Invoke((MethodInvoker)(() => { rawEntryListView.EndUpdate(); }));

				Invoke((MethodInvoker)(() => { _rawListViewUpdaterThread = null; }));
			}
		}

		private void AuctionIdListAddItem(string listingId, ListViewItem listingListViewItem, bool triggerChangeEvent = true)
		{
			Invoke((MethodInvoker)(() => { rawEntryListView.Items.Add(listingListViewItem); }));
			if (triggerChangeEvent)
				Invoke((MethodInvoker)(() => { RawEntryListViewChanged(); }));
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			try
			{
				int addCount = 0;
				foreach (string listingId in _listingIds)
				{
					AddListingIdAction?.Invoke(listingId);
					addCount++;
				}
				if (addCount > 0)
				ListingIdsAddedAction?.Invoke();
			}
			catch (Exception ex)
			{
				_dataManager.LogException(ex);
			}
			finally
			{
				textBoxRawData.Text = _textBoxRawDataDefaultText;
				this.Close();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			try
			{
				textBoxRawData.Text = _textBoxRawDataDefaultText;
			}
			catch (Exception ex)
			{
				_dataManager.LogException(ex);
			}
			finally
			{
				textBoxRawData.Text = _textBoxRawDataDefaultText;
				this.Close();
			}
		}
	}
}
