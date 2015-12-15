using Community.CsharpSqlite.SQLiteClient;
using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using eBay.Service.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EbayLeaveBulkFeedback
{
	public class DataManager
	{
		const int PICK_SUBITEM_TITLE = 0;
		const int PICK_SUBITEM_DATE = 1;
		const int PICK_SUBITEM_ITEM_ID = 2;

		private Dictionary<string, ListViewItem> _masterPickList;
		private Dictionary<string, ListViewItem> _feedbackList;
		public ListView FeedbackListView { get; set; }
		public ListView PickListView { get; set; }
		public RichTextBox TextBoxRawFeedbackData;
		private string _searchString;
		private string[] _searchStrings;
		public string SearchString
		{
			get { return _searchString; }
			set 
			{ 
				_searchString = value;
				_searchStrings = _searchString.Split(' ');
				UpdateListViewAsync();
			}
		}
		public Control MainForm { get; set; }
		public Control PickDialog { get; set; }
		public HashSet<string> SelectedItems { get; set; }
		public Action FeedbackListViewChanged { get; set; }
		public Action PickListViewChanged { get; set; }
		public Func<Bitmap, int> AddPickListViewImage { get; set; }

		Thread _listViewUpdaterThread;
		Thread _initPickListViewThread;
		Thread _leaveFeedbackThread;

		public DataManager()
		{
			_masterPickList = new Dictionary<string, ListViewItem>();
			_feedbackList = new Dictionary<string, ListViewItem>();
		}

		private SQLiteDatabase _db;
		public SQLiteDatabase DB
		{
			get
			{
				if (_db == null)
				{
					_db = new SQLiteDatabase(ConfigurationManager.AppSettings["SqliteDataFile"]);
					VerifyDb();
				}

				return _db;
			}
		}

		private void VerifyDb()
		{
			string sql = "SELECT NAME FROM SQLITE_MASTER WHERE type='table' AND name='EbayItemsAwaitingFeedback';";
			if (_db.GetDataRow(sql) == null)
			{
				// Init the new database
				string createSql = @"CREATE TABLE [EbayItemsAwaitingFeedback] (
						[ItemId] TEXT NOT NULL PRIMARY KEY,
						[TransactionId] TEXT NULL,
						[OrderLineItemId] TEXT NULL,
						[Title] TEXT NOT NULL,
						[Seller] TEXT NULL,
						[GalleryImageUrl] TEXT NULL,
						[GalleryImage] BLOB NULL,
						[EndDateTime] TIMESTAMP NOT NULL,
						[CreateDateTime] TIMESTAMP NOT NULL,
						[Status] TEXT NULL,
						[FeedbackLeft] TEXT NULL,
						[ProfileName] TEXT NULL
					);

					CREATE UNIQUE INDEX [IDX_EbayItemsAwaitingFeedback_ItemId] ON [EbayItemsAwaitingFeedback](
						[ItemId] ASC
					);
					";

				_db.ExecuteNonQuery(createSql);
			}
		}

		public void InitPickListView(object isThread = null)
		{
			if (isThread == null)
			{
				if (_initPickListViewThread != null && _initPickListViewThread.IsAlive)
				{
					_initPickListViewThread.Abort();
				}

				_initPickListViewThread = new Thread(new ParameterizedThreadStart(InitPickListView)) { IsBackground = true };
				_initPickListViewThread.Start(true);
				return;
			}

			try
			{
				// load all records from database that have Status = '' to _listView
				lock (DB)
				{
					string sql = @"	SELECT ItemId, Title, Seller, TransactionId, OrderLineItemId, EndDateTime, ProfileName
									FROM EbayItemsAwaitingFeedback 
									WHERE Status IS NULL OR Status = ''
									ORDER BY EndDateTime";

					var items = DB.GetDataTable(sql);
					foreach (DataRow item in items.Rows)
					{
						var itemSummary = GetItemSummary(item);
						MasterPickListAddItem(itemSummary);
					}
				}

				foreach (var listViewItemKeyValue in _masterPickList)
				{
					UpdatePickListImage(listViewItemKeyValue.Value);
				}

				// Get items available for feedback from ebay and check against our existing list
				var availableFeedbackItemProcessor = new AvailableFeedbackItemProcessor(DB);
				availableFeedbackItemProcessor.GetAvailableFeedbackItems(MasterPickListAddItemAction);

				_initPickListViewThread = null;
			}
			catch (Exception ex)
			{
				LogException(ex);
			}
		}

		private void UpdatePickListImage(ListViewItem listViewItem)
		{
			string sql = @"SELECT GalleryImage 
					FROM EbayItemsAwaitingFeedback 
					WHERE ItemId = '" + listViewItem.SubItems[2].Text.Replace("'", "''") + @"'";
			DataRow row;
			lock (DB)
			{
				row = DB.GetDataRow(sql, new DataColumn[] { new DataColumn("GalleryImage", typeof(byte[])) });
			}
			if (row != null)
			{
				var galleryImageBytes = (byte[])row[0];
				Bitmap galleryImage = null;

				if (galleryImageBytes != null && galleryImageBytes.Length > 0)
				{
					try
					{
						using (MemoryStream stream = new MemoryStream(galleryImageBytes))
						{
							galleryImage = (Bitmap)Bitmap.FromStream(stream);
						}
					}
					catch (Exception ex)
					{
						LogException(ex);
						return;
					}
				}
				int imageIndex = AddPickListViewImage(galleryImage);
				PickDialog.Invoke((MethodInvoker)(() => { listViewItem.ImageIndex = imageIndex; }));
			}
		}

		private void MasterPickListAddItemAction(string itemId, string transactionId, string orderLineItemId, string profileName)
		{
			ListViewItem listViewItem;
			if (_masterPickList.TryGetValue(itemId, out listViewItem))
				return;

			EbayItemSummary itemSummary = GetItemSummary(itemId, transactionId, orderLineItemId, profileName);
			if (itemSummary == null)
				return;	// item has a processed status or ebay couldn't find it
			listViewItem = MasterPickListAddItem(itemSummary);
			if (listViewItem == null)
				return;	// duplicate?
			UpdatePickListImage(listViewItem);
		}

		private EbayItemSummary GetItemSummary(string itemId, string transactionId, string orderLineItemId, string profileName)
		{
			string sql = @"	SELECT ItemId, Title, Seller, TransactionId, OrderLineItemId, EndDateTime, ProfileName, Status
							FROM EbayItemsAwaitingFeedback 
							WHERE ItemId = '" + itemId.Replace("'", "''") + @"'";	// AND (Status IS NULL OR Status = '')";
			DataRow row;
			lock (DB)
			{
				row = DB.GetDataRow(sql);
			}

			if (row != null)
			{
				if (!string.IsNullOrEmpty(row[7] as string))
					return null;	// The status indicates we have already processed this item
				return GetItemSummary(row);
			}
			else
			{
				// Get from ebay
				return GetItemSummaryFromEbay(itemId, transactionId, orderLineItemId, profileName);
			}
		}

		private EbayItemSummary GetItemSummaryFromEbay(string itemId, string transactionId, string orderLineItemId, string profileName)
		{
			EbayItemSummary itemSummary = null;

			try
			{
				var ebayUserToken = ConfigurationManager.AppSettings["EbayUserToken" + (profileName == "1" ? string.Empty : profileName)];
				var apiContext = GetApiContext(ebayUserToken);
				var getItemCall = new eBay.Service.Call.GetItemCall(apiContext);// { TransactionID = orderLineItemId };
				var itemDetails = getItemCall.GetItem(itemId);

				Bitmap image = null;
				try
				{
					if (!string.IsNullOrEmpty(itemDetails.PictureDetails.GalleryURL))
					{
						var request = WebRequest.Create(itemDetails.PictureDetails.GalleryURL);
						using (var response = request.GetResponse())
						using (var stream = response.GetResponseStream())
						{
							image = (Bitmap)Bitmap.FromStream(stream);
						}
					}

					itemSummary = new EbayItemSummary()
					{
						ItemId = itemId,
						TransactionId = transactionId,
						OrderLineItemId = orderLineItemId,
						Title = itemDetails.Title,
						Seller = itemDetails.Seller.UserID,
						GalleryImageUrl = itemDetails.PictureDetails.GalleryURL,
						ProfileName = profileName,
						EndDateTime = itemDetails.ListingDetails.EndTime
					};

					var data = new Dictionary<string, object>();
					data["ItemId"] = itemId;
					data["TransactionId"] = transactionId;
					data["OrderLineItemId"] = orderLineItemId;
					data["Title"] = itemSummary.Title;
					data["Seller"] = itemSummary.Seller;
					data["GalleryImageUrl"] = itemSummary.GalleryImageUrl;
					data["GalleryImage"] = (image == null ? null : (byte[])new ImageConverter().ConvertTo(image, typeof(byte[])));
					data["ProfileName"] = itemSummary.ProfileName;
					data["EndDateTime"] = itemSummary.EndDateTime;
					data["CreateDateTime"] = DateTime.Now;

					lock (DB)
					{
						DB.Insert("EbayItemsAwaitingFeedback", data);
					}
				}
				catch (Exception ex)
				{
					LogException(ex);
				}
			}
			catch (Exception ex)
			{
				LogException(ex);
			}

			return itemSummary;
		}

		private ListViewItem MasterPickListAddItem(EbayItemSummary itemSummary)
		{
			ListViewItem listViewItem;
			if (_masterPickList.TryGetValue(itemSummary.ItemId, out listViewItem))
				return null;	// no dulpicates

			//lock (_pickListViewLock)
			{
				listViewItem = CreatePickListViewItem(itemSummary);
				_masterPickList[itemSummary.ItemId] = listViewItem;
				if (IsFilterMatch(listViewItem))
				{
					PickDialog.Invoke((MethodInvoker)(() =>
					{
						PickListView.Items.Add(listViewItem);
						PickListViewChanged();
					}));
				}

				return listViewItem;
			}
		}

		private ListViewItem CreatePickListViewItem(EbayItemSummary itemSummary)
		{
			try
			{
				if (itemSummary == null)
					return null;

				//if (itemSummary.GalleryImage == null)
				//	return null;

				//int imageIndex = AddPickListViewImage(itemSummary.GalleryImage);

				var listViewItem = new ListViewItem(itemSummary.Title);
				listViewItem.SubItems.Add(itemSummary.EndDateTime.ToString("yyyy-MM-dd"));
				listViewItem.SubItems.Add(itemSummary.ItemId);
				listViewItem.SubItems.Add(itemSummary.Seller);
				listViewItem.SubItems.Add(itemSummary.ProfileName);
				ApplyPickViewItemStyle(listViewItem);

				return listViewItem;
			}
			catch (Exception ex)
			{
				LogException(ex);
			}

			return null;
		}

		private EbayItemSummary GetItemSummary(DataRow row)
		{
			EbayItemSummary itemSummary = new EbayItemSummary()
			{	// ItemId, Title, Seller, TransactionId, OrderLineItemId, EndDateTime, ProfileName
				ItemId = (string)row[0],
				Title = (string)row[1],
				Seller = (string)row[2],
				TransactionId = (string)row[3],
				OrderLineItemId = (string)row[4],
				EndDateTime = DateTime.Parse((string)row[5]),
				ProfileName = (string)row[6],
			};

			return itemSummary;
		}

		public void ProcessSelectedPickListItems(Action<string> processListItem)
		{
			if (processListItem != null && PickListView.SelectedItems.Count > 0)
			{
				foreach (ListViewItem listItem in PickListView.SelectedItems)
				{
					string title = listItem.SubItems[PICK_SUBITEM_TITLE].Text;
					string date = listItem.SubItems[PICK_SUBITEM_DATE].Text;
					string itemId = listItem.SubItems[PICK_SUBITEM_ITEM_ID].Text;
					processListItem(itemId + " - " + date + " - " + title);

					listItem.BackColor = Color.Yellow;
				}
			}
		}

		private void ApplyPickViewItemStyle(ListViewItem listViewItem)
		{
			if (SelectedItems != null)
			{
				if (SelectedItems.Contains(listViewItem.SubItems[2].Text))
					listViewItem.BackColor = Color.Yellow;
			}
		}

		private bool IsFilterMatch(ListViewItem listViewItem)
		{
			if (_searchStrings == null)
				return true;

			foreach (string searchString in _searchStrings)
			{
				bool found =
					listViewItem.SubItems[0].Text.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0		// Title
					|| listViewItem.SubItems[2].Text.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0		// ItemId
					|| listViewItem.SubItems[3].Text.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0;	// Seller
				
				if (!found)
					return false;
			}

			return true;

			//if (string.IsNullOrEmpty(SearchString))
			//	return true;

			//return listViewItem.SubItems[0].Text.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) >= 0		// Title
			//	|| listViewItem.SubItems[2].Text.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) >= 0		// ItemId
			//	|| listViewItem.SubItems[3].Text.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) >= 0;	// Seller
		}

		private Thread _updateListViewAsyncThread;
		public void UpdateListViewAsync()
		{
			if (_updateListViewAsyncThread != null && _updateListViewAsyncThread.IsAlive)
			{
				_updateListViewAsyncThread.Abort();
			}

			_updateListViewAsyncThread = new Thread(new ThreadStart(UpdateListView)) { IsBackground = true };
			_updateListViewAsyncThread.Start();
		}

		public void UpdateListView()
		{
			try
			{
				// Apply SearchString...
				// Make a filteredList from the master dictionary
				var filteredList = _masterPickList.Where(item => { return IsFilterMatch(item.Value); })
					.ToDictionary(p => p.Key, p => p.Value);

				// Loop through the _listView:
				int itemCount = 0;
				PickDialog.Invoke((MethodInvoker)(() => 
				{
					PickListView.BeginUpdate();
					itemCount = PickListView.Items.Count;
				}));
				for (int i = itemCount - 1; i >= 0; i--)
				{
					ListViewItem listViewItem = null;
					PickDialog.Invoke((MethodInvoker)(() => 
					{
						listViewItem = PickListView.Items[i];
					}));
					ListViewItem foundListViewItem;
					string itemId = listViewItem.SubItems[2].Text;
					if (filteredList.TryGetValue(itemId, out foundListViewItem))
					{
						// remove filteredList items as they are found
						filteredList.Remove(itemId);
					}
					else
					{
						// remove list view items not in the filteredList 
						PickDialog.Invoke((MethodInvoker)(() => 
						{
							PickListView.Items.RemoveAt(i);
							PickListViewChanged();
						}));
					}
				}

				// Add all items in filteredList to the _listView
				foreach (var keyValue in filteredList)
				{
					PickDialog.Invoke((MethodInvoker)(() => 
					{
						PickListView.Items.Add(keyValue.Value);
						PickListViewChanged();
					}));
				}
			}
			catch (Exception ex)
			{
				LogException(ex);
			}
			finally
			{
				PickDialog.Invoke((MethodInvoker)(() => { PickListView.EndUpdate(); }));
			}
		}

		internal void LogException(Exception ex)
		{
			// TODO: log the exception
			int breakHere = 1;
			breakHere++;
		}

		public void FeedbackUpdate(string itemId, FeedbackUpdates updates)
		{
			ListViewItem listViewItem;
			if (_feedbackList.TryGetValue(itemId, out listViewItem))
			{
				var data = new Dictionary<string, object>();
				if (updates.Status != null) MainForm.Invoke((MethodInvoker)(() => 
				{ 
					listViewItem.SubItems[0].Text = updates.Status;
					data["Status"] = updates.Status;
				}));
				if (updates.Title != null) MainForm.Invoke((MethodInvoker)(() => { listViewItem.SubItems[2].Text = updates.Title; }));
				if (updates.Seller != null) MainForm.Invoke((MethodInvoker)(() => { listViewItem.SubItems[3].Text = updates.Seller; }));
				if (updates.FeedbackLeft != null) MainForm.Invoke((MethodInvoker)(() => 
				{ 
					listViewItem.SubItems[4].Text = updates.FeedbackLeft;
					data["FeedbackLeft"] = updates.FeedbackLeft;
				}));
				if (updates.Result != null) MainForm.Invoke((MethodInvoker)(() => { listViewItem.SubItems[5].Text = updates.Result; }));
				if (data.Count > 0)
				{
					lock (DB)
					{
						DB.Update("EbayItemsAwaitingFeedback", data, "ItemId='" + itemId.Replace("'", "''") + "'");
					}
				}
			}
		}

		public void UpdateFeedbackListViewAsync(object isThread = null)
		{
			if (isThread == null)
			{
				if (_listViewUpdaterThread != null && _listViewUpdaterThread.IsAlive)
				{
					_listViewUpdaterThread.Abort();
				}

				_listViewUpdaterThread = new Thread(new ParameterizedThreadStart(UpdateFeedbackListViewAsync)) { IsBackground = true };
				_listViewUpdaterThread.Start(true);
				return;
			}

			try
			{
				string[] itemIds = null;
				MainForm.Invoke((MethodInvoker)(() => { itemIds = Helpers.ExtractStringsByRegex(TextBoxRawFeedbackData.Text, "(\\d+)"); }));
				itemIds = itemIds.Where((itemId) => { return itemId.Length == 12; }).ToArray();
				//HashSet<string> hashItemIds = new HashSet<string>();
				//foreach (string itemId in itemIds)
				//{
				//	hashItemIds.Add(itemId);
				//}
				var newFeedbackList = new Dictionary<string, ListViewItem>();

				foreach (var itemId in itemIds)
				{
					ListViewItem listViewItem;
					if (newFeedbackList.TryGetValue(itemId, out listViewItem))
						continue;	// no dupes

					if (_feedbackList.TryGetValue(itemId, out listViewItem))
					{
						// we already had this item so keep it
						newFeedbackList[itemId] = listViewItem;
					}
					else
					{
						listViewItem = GetFeedbackListViewItem(itemId);
						if (listViewItem != null)
							newFeedbackList[itemId] = listViewItem;
					}
				}

				_feedbackList.Clear();
				MainForm.Invoke((MethodInvoker)(() => { FeedbackListView.BeginUpdate(); FeedbackListView.Items.Clear(); }));
				foreach (var newFeedback in newFeedbackList)
				{
					FeedbackListAddItem(newFeedback.Key, newFeedback.Value, false);
				}
				MainForm.Invoke((MethodInvoker)(() => { FeedbackListView.EndUpdate(); FeedbackListViewChanged(); }));

				MainForm.Invoke((MethodInvoker)(() => { _listViewUpdaterThread = null; }));
			}
			catch (Exception ex)
			{
				LogException(ex);
			}
			finally
			{
				MainForm.Invoke((MethodInvoker)(() => { FeedbackListView.EndUpdate(); }));
			}
		}
		/*
		private void FeedbackListAddItem(string itemId)
		{
			var listViewItem = GetFeedbackListViewItem(itemId);
			_feedbackList[itemId] = listViewItem;

			MainForm.Invoke((MethodInvoker)(() => { FeedbackListView.Items.Add(listViewItem); FeedbackListViewChanged(); }));
		}
		*/
		private void FeedbackListAddItem(string itemId, ListViewItem listViewItem, bool triggerChangeEvent = true)
		{
			_feedbackList[itemId] = listViewItem;

			MainForm.Invoke((MethodInvoker)(() => { FeedbackListView.Items.Add(listViewItem); }));
			if (triggerChangeEvent)
				MainForm.Invoke((MethodInvoker)(() => { FeedbackListViewChanged(); }));
		}

		private ListViewItem GetFeedbackListViewItem(string itemId)
		{
			string status			= null;
			string title			= null;
			string sellerName		= null;
			string feedbackLeft		= null;
			string transactionId	= null;
			string orderLineItemId	= null;
			string profileName		= null;

			// Get from DB
			string sql = @"	SELECT Status, Title, Seller, FeedbackLeft, TransactionId, OrderLineItemId, ProfileName
							FROM EbayItemsAwaitingFeedback 
							WHERE ItemId = '" + itemId.Replace("'", "''") + @"'";
			DataRow row;
			lock (DB)
			{
				row = DB.GetDataRow(sql);
			}

			if (row != null)
			{
				status			= row[0] as string;
				title			= row[1] as string;
				sellerName		= row[2] as string;
				feedbackLeft	= row[3] as string;
				transactionId	= row[4] as string;
				orderLineItemId	= row[5] as string;
				profileName		= row[6] as string;
			}
			var newItem = new string[] { status, itemId, title, sellerName, feedbackLeft, null, transactionId, orderLineItemId, profileName };
			var listViewItem = new ListViewItem(newItem);

			return listViewItem;
		}


		public Thread LeaveFeedbacksAsync(Action before, Action after, Action<string, int?> generalStatusUpdate)
		{
			if (_leaveFeedbackThread != null && _leaveFeedbackThread.IsAlive)
			{
				_leaveFeedbackThread.Abort();
			}
			_leaveFeedbackThread = new Thread(new ParameterizedThreadStart(LeaveFeedbacks)) { IsBackground = true };
			_leaveFeedbackThread.Start(new object[] { before, after, generalStatusUpdate });

			return _leaveFeedbackThread;
		}

		private void LeaveFeedbacks(object args)
		{
			var argsArray = (object[])args;
			Action before = (Action)argsArray[0];
			Action after = (Action)argsArray[1];
			Action<string, int?> generalStatusUpdate = (Action<string, int?>)argsArray[2];

			MainForm.Invoke((MethodInvoker)(() => { before(); }));
			
			try
			{
				var feedbackToSellers = ConfigurationManager.AppSettings["FeedbackToSellers"].Split('\n').Select(x => x.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
				Random random = new Random();
				foreach (var feedbackItem in _feedbackList)
				{
					string status = feedbackItem.Value.SubItems[0].Text;
					if (!(string.IsNullOrEmpty(status) || status == "Error"))
						continue;

					string profileName = feedbackItem.Value.SubItems[8].Text;
					var ebayUserToken = ConfigurationManager.AppSettings["EbayUserToken" + (profileName == "1" ? string.Empty : profileName)];
					if (string.IsNullOrEmpty(ebayUserToken))
						return;

					string sellerName = feedbackItem.Value.SubItems[3].Text;
					string transactionId = feedbackItem.Value.SubItems[6].Text;
					string orderLineItemId = feedbackItem.Value.SubItems[7].Text;
					var apiContext = GetApiContext(ebayUserToken);
					string feedback = feedbackToSellers[random.Next(0, feedbackToSellers.Length - 1)];
					LeaveFeedback(feedbackItem.Key, transactionId, orderLineItemId, sellerName, apiContext, feedback);
				}
			}
			catch (Exception ex)
			{
				LogException(ex);
			}

			MainForm.Invoke((MethodInvoker)(() => 
			{
				after();
			}));
			_leaveFeedbackThread = null;
		}

		private void LeaveFeedback(string itemId, string transactionId, string orderLineItemId, string giveFeedbackTo, ApiContext apiContext, string feedback)
		{
			try
			{
				var itemRatingDetailsTypeCollection = new ItemRatingDetailsTypeCollection();
				itemRatingDetailsTypeCollection.Add(new ItemRatingDetailsType() { Rating = 5, RatingDetail = FeedbackRatingDetailCodeType.Communication });
				itemRatingDetailsTypeCollection.Add(new ItemRatingDetailsType() { Rating = 5, RatingDetail = FeedbackRatingDetailCodeType.ItemAsDescribed });
				itemRatingDetailsTypeCollection.Add(new ItemRatingDetailsType() { Rating = 5, RatingDetail = FeedbackRatingDetailCodeType.ShippingAndHandlingCharges });
				itemRatingDetailsTypeCollection.Add(new ItemRatingDetailsType() { Rating = 5, RatingDetail = FeedbackRatingDetailCodeType.ShippingTime });

				var leaveFeedbackCall = new LeaveFeedbackCall(apiContext)
				{
					//ItemID = itemId,
					//CommentText = feedback,
					//CommentType = CommentTypeCodeType.Positive,
					//TransactionID = transactionId,
					//TargetUser = giveFeedbackTo,
					SellerItemRatingDetailArrayList = itemRatingDetailsTypeCollection,
					//OrderLineItemID = orderLineItemId,
					ItemArrivedWithinEDDType = ItemArrivedWithinEDDCodeType.BuyerIndicatedItemArrivedWithinEDDRange
				};

				string result = leaveFeedbackCall.LeaveFeedback(giveFeedbackTo, itemId, CommentTypeCodeType.Positive, feedback);
				/*
				string result = leaveFeedbackCall.LeaveFeedback(
					itemId,
					feedback,
					CommentTypeCodeType.Positive,
					transactionId,
					giveFeedbackTo,
					itemRatingDetailsTypeCollection,
					orderLineItemId,
					ItemArrivedWithinEDDCodeType.BuyerIndicatedItemArrivedWithinEDDRange);	// eBay.Service.Core.Soap
				*/

				var updates = new FeedbackUpdates()
				{
					FeedbackLeft = feedback,
					Status = "Done",
					Result = result
				};
				FeedbackUpdate(itemId, updates);
			}
			catch (Exception ex)
			{
				string status;
				if (ex.Message.Contains("feedback has been left already"))	
				{
					status = "Done";
				}
				else
				{
					status = "Error";
				}
				var updates = new FeedbackUpdates()
				{
					Status = status,
					Result = ex.Message
				};
				FeedbackUpdate(itemId, updates);
			}
		}

		/// <summary>
		/// Populate eBay SDK ApiContext object with data from application configuration file
		/// </summary>
		/// <returns>ApiContext object</returns>
		ApiContext GetApiContext(string ebayUserToken)
		{
			//set Api Token to access eBay Api Server
			ApiCredential apiCredential = new ApiCredential()
			{
				eBayToken = ebayUserToken
			};

			//set Api logging
			ApiLogManager apiLogManager = new ApiLogManager() { EnableLogging = true };
			//apiLogManager.ApiLoggerList.Add(new FileLogger("ebayApiLog.txt", true, true, true));

			var apiContext = new ApiContext()
			{
				ApiCredential = apiCredential,
				//set eBay Site target to US
				Site = SiteCodeType.US,
				ApiLogManager = apiLogManager
			};

			return apiContext;
		}
	}
}
