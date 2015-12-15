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

		public ItemPickDialog(DataManager dataManager)
		{
			InitializeComponent();
			pickListView.LargeImageList = new ImageList()
			{
				ImageSize = new Size(140, 140),
				ColorDepth = ColorDepth.Depth32Bit
			};
			pickListView.ListViewItemSorter = new ListViewItemComparer(1);	// date
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
				return;	// Do not close if the user hit the close button

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
			pickListView.SelectedItems.Clear();	// prevent the scrolling from jumping around
			if (pickListView.FocusedItem != null)
				pickListView.FocusedItem.Focused = false;	// prevent the scrolling from jumping around
		}
		
		private void UpdateItemCount()
		{
			toolStripItemCount.Text = "Items: " + pickListView.Items.Count.ToString();
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
	}
}
