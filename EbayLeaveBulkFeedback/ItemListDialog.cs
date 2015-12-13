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
	public partial class ItemListDialog : Form
	{
		Thread _populateItems;
		private object _listViewItemsLock = new object();
		public Action<string> DoubleClickAction { get; set; }
		public HashSet<string> SelectedItems { get; set; }

		public ItemListDialog()
		{
			InitializeComponent();
			listViewItems.LargeImageList = new ImageList()
			{
				ImageSize = new Size(140, 140),
				ColorDepth = ColorDepth.Depth32Bit
			};
			listViewItems.ListViewItemSorter = new ListViewItemComparer(1);	// date
			listViewItems.Sort();
			this.listViewItems.Scroll += new ScrollEventHandler(listViewItems_Scroll);
			PopulateItems();
		}

		private void ItemListDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason != CloseReason.UserClosing) 
				return;	// Do not close if the user hit the close button

			Hide();
			e.Cancel = true;
		}

		private void PopulateItems(object isThread = null)
		{
			if (isThread == null)
			{
				if (_populateItems != null && _populateItems.IsAlive)
				{
					_populateItems.Abort();
				}

				_populateItems = new Thread(new ParameterizedThreadStart(PopulateItems));
				_populateItems.Start(true);
				return;
			}

			try
			{
				var availableFeedbackItemProcessor = new AvailableFeedbackItemProcessor();
				availableFeedbackItemProcessor.ProcessAvailableFeedbackItems(AddItem);

				Invoke((MethodInvoker)(() => { _populateItems = null; }));
			}
			catch { }
		}

		public void AddItem(string profileName, EbayItemSummary itemSummary)	//, string itemId, string imageUrl, string title)
		{
			try
			{
				if (itemSummary == null)
					return;

				if (itemSummary.GalleryImage == null)
					return;
					//itemSummary.GalleryImage = new Bitmap();

				lock (_listViewItemsLock)
				{
					Invoke((MethodInvoker)(() =>
					{
						listViewItems.LargeImageList.Images.Add(itemSummary.GalleryImage);
						int imageIndex = listViewItems.LargeImageList.Images.Count - 1;

						var listViewItem = new ListViewItem(itemSummary.Title, imageIndex);
						listViewItems.BeginUpdate();
						listViewItem.SubItems.Add(itemSummary.EndDateTime.ToString("yyyy-MM-dd"));
						listViewItem.SubItems.Add(itemSummary.ItemId);
						listViewItem.SubItems.Add(itemSummary.Seller);
						listViewItem.SubItems.Add(profileName);
						Filter(listViewItem);

						listViewItems.Items.Add(listViewItem);
						toolStripItemCount.Text = "Items: " + listViewItems.Items.Count.ToString();

						listViewItems.EndUpdate();
					}));
				}
			}
			catch (Exception ex)
			{
				int i = 0;
				i++;
			}
		}

		private void Filter(ListViewItem listViewItem)
		{
			if (SelectedItems != null)
			{
				if (SelectedItems.Contains(listViewItem.SubItems[2].Text))
					listViewItem.BackColor = Color.Yellow;
			}

			/*
			if (listViewItems.Items.Remove())
				Invoke((MethodInvoker)(() =>
				{
				}));
			 * */
		}

		private void listViewItems_DoubleClick(object sender, EventArgs e)
		{
			if (DoubleClickAction != null && listViewItems.SelectedItems.Count != 0)
			{
				var listItem = listViewItems.SelectedItems[0];
				string title = listItem.SubItems[0].Text;
				string date = listItem.SubItems[1].Text;
				string itemId = listItem.SubItems[2].Text;
				DoubleClickAction(itemId + " - " + date + " - " + title);

				listItem.BackColor = Color.Yellow;
			}
		}
		
		private void listViewItems_Scroll(object sender, ScrollEventArgs e)
		{
			//lock (_listViewItemsLock)
			//{
				//Invoke((MethodInvoker)(() =>
				//{
					listViewItems.SelectedItems.Clear();	// prevent the scrolling from jumping around
					if (listViewItems.FocusedItem != null)
						listViewItems.FocusedItem.Focused = false;	// prevent the scrolling from jumping around
				//}));
			//}
		}
	}
}
