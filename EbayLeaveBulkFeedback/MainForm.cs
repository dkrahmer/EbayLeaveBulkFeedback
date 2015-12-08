using EbayLeaveBulkFeedback;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ebayLeaveFeedbackForSellers
{
	public partial class MainForm : Form
	{
		Dictionary<string, ListViewItem> _items;
		Thread _listViewUpdaterThread;
		Thread _leaveFeedbackThread;

		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			ResetGui();
			UpdateListViewAsync();
		}

		private void textBoxRawData_TextChanged(object sender, EventArgs e)
		{
			UpdateListViewAsync();
		}

		private void UpdateListViewAsync(object isThread = null)
		{
			if (isThread == null)
			{
				if (_listViewUpdaterThread != null && _listViewUpdaterThread.IsAlive)
				{
					_listViewUpdaterThread.Abort();
				}

				_listViewUpdaterThread = new Thread(new ParameterizedThreadStart(UpdateListViewAsync));
				_listViewUpdaterThread.Start(true);
				return;
			}
			//var itemIds = Helpers.ExtractNumberStrings(textBoxRawData.Text, "(?i)(([^\\d]|^)?(?<target>\\d{12,13})([^\\d]|$)?)+");
			// var itemIds = Helpers.ExtractNumberStrings(textBoxRawData.Text, "(\\d+)");

			string[] itemIds = null;
			Invoke((MethodInvoker)(() => { itemIds = Helpers.ExtractStringsByRegex(textBoxRawData.Text, "(\\d+)"); }));

			_items = new Dictionary<string, ListViewItem>();
			_items.Clear();

			Invoke((MethodInvoker)(() => { listViewItems.Items.Clear(); }));
			foreach (var itemId in itemIds)
			{
				if (itemId.Length < 12 || itemId.Length > 12)	// allow for a length range
					continue;
				ListViewItem listViewItem;
				if (_items.TryGetValue(itemId, out listViewItem))
					continue;	// no dupes

				var newItem = new string[] { null, itemId, null, null, null, null };
				listViewItem = new ListViewItem(newItem);
				_items.Add(itemId, listViewItem);

				Invoke((MethodInvoker)(() => { listViewItems.Items.Add(listViewItem); }));
			}
		}

		private void buttonLeaveFeedback_Click(object sender, EventArgs e)
		{
			LeaveFeedbackAsync();
		}

		private void LeaveFeedbackAsync(object isThread = null)
		{
			if (isThread == null)
			{
				if (_leaveFeedbackThread != null && _leaveFeedbackThread.IsAlive)
				{
					_leaveFeedbackThread.Abort();
				}

				_leaveFeedbackThread = new Thread(new ParameterizedThreadStart(LeaveFeedbackAsync));
				_leaveFeedbackThread.Start(true);
				return;
			}

			Invoke((MethodInvoker)(() => { EnableDisableAll(false); }));
			
			var ebayLeaveFeedback = new EbayLeaveFeedback();
			var items = new HashSet<string>(_items.Keys);
			try
			{
				int feedbacksLeft = ebayLeaveFeedback.LeaveFeedbacks(items, GeneralStatusUpdate, FeedbackUpdate);
			}
			catch (Exception ex)
			{

			}

			Invoke((MethodInvoker)(() => { ResetGui(); }));
		}

		private void ResetGui()
		{
			EnableDisableAll(true);
			GeneralStatusUpdate("Ready", 0);
		}

		private void EnableDisableAll(bool enabled)
		{
			textBoxRawData.Enabled = enabled;
			buttonLeaveFeedback.Enabled = enabled;
			buttonStop.Enabled = !enabled;
		}

		private void GeneralStatusUpdate(string status = null, int percentComplete = -1)
		{
			if (status != null)
				Invoke((MethodInvoker)(() => { ((ToolStripStatusLabel)(statusStrip.Items["toolStripStatusLabel"])).Text = status; }));

			if (percentComplete >= 0)
				Invoke((MethodInvoker)(() => { ((ToolStripProgressBar)(statusStrip.Items["toolStripProgressBar"])).Value = percentComplete; }));
		}

		private void FeedbackUpdate(string itemId, FeedbackUpdates updates)
		{
			ListViewItem listViewItem;
			if (_items.TryGetValue(itemId, out listViewItem))
			{
				if (updates.Status != null)			listViewItem.SubItems[0].Text = updates.Status;
				//if (updates.ItemId != null)			listViewItem.SubItems[1].Text = updates.ItemId;
				if (updates.Title != null)			listViewItem.SubItems[2].Text = updates.Title;
				if (updates.Seller != null)			listViewItem.SubItems[3].Text = updates.Seller;
				if (updates.FeedbackLeft != null)	listViewItem.SubItems[4].Text = updates.FeedbackLeft;
				if (updates.Result != null)			listViewItem.SubItems[5].Text = updates.Result;
			}
		}

		private void buttonStop_Click(object sender, EventArgs e)
		{
			if (_leaveFeedbackThread != null && _leaveFeedbackThread.IsAlive)
			{
				_leaveFeedbackThread.Abort();
			}

			ResetGui();
		}
	}
}
