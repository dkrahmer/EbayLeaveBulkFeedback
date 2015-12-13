using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using eBay.Service.Util;
using System.Text.RegularExpressions;
using eBay.Services;
using System.Threading;
using EbayLeaveBulkFeedback;
using System.Drawing;
using System.Net;
using System.IO;
using System.Data;

namespace EbayLeaveBulkFeedback
{
	public class AvailableFeedbackItemProcessor
	{
		public AvailableFeedbackItemProcessor()
		{
		}

		public int ProcessAvailableFeedbackItems(Action<string, EbayItemSummary> each_ProfileName_Item)
		{
			// Get EbayUserTokens from Web.config
			var ebayUserTokens = new List<string>();
			int totalItems = 0;
			for (int i = 1; ; i++)
			{
				var ebayUserToken = ConfigurationManager.AppSettings["EbayUserToken" + (i < 2 ? string.Empty : i.ToString())];
				if (string.IsNullOrEmpty(ebayUserToken))
					break;

				// Create a service client
				var apiContext = GetApiContext(ebayUserToken);
				totalItems += ProcessAvailableFeedbackItems(apiContext, each_ProfileName_Item, profileName: i.ToString());
			}

			return totalItems;
		}

		//public int LeaveFeedbacks(ApiContext apiContext, HashSet<string> itemIds, Action<string, int?> generalStatusUpdate = null, Action<string, FeedbackUpdates> feedbackUpdate = null, string profileName = null)
		public int ProcessAvailableFeedbackItems(ApiContext apiContext, Action<string, EbayItemSummary> each_ProfileName_Item, string profileName)
		{
			/*
			string tmpId = Guid.NewGuid().ToString();
			var itemSummary3 = new EbayItemSummary()
			{
				ItemId = tmpId,
				TransactionId = "0",
				GalleryImageUrl = "http://test/",
				GalleryImage = new Bitmap(@"D:\dl\howto_listview_icons\1_64x64.png"),//new byte[] {1,2,3,4,5,6,7,8,9,0},
				Title = "My Title 123",
				EndDateTime = DateTime.Now
			};

			SetCachedItemSummary(itemSummary3);
			var summ = GetCachedItemSummary(tmpId, "0");
			return 2;

			var awaitingFeedbackItem2 = new TransactionType();
			awaitingFeedbackItem2.Item = new ItemType() { ItemID = "191747590750" };
			awaitingFeedbackItem2.TransactionID = "0";
			var itemSummary2 = GetItemSummary(awaitingFeedbackItem2, apiContext);
			
			return 1;
			 * 
			 * */


			string baseMessage = (profileName == null ? string.Empty : "Profile " + profileName + ": ");
			//string baseMessageGettingListOfItems = "Getting list of item transactions that need feedback...";
			//if (generalStatusUpdate != null)
			//	generalStatusUpdate(baseMessage + baseMessageGettingListOfItems, 0);

			var feedbackToSellers = ConfigurationManager.AppSettings["FeedbackToSellers"].Split('\n').Select(x => x.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
			var feedbackToBuyers = ConfigurationManager.AppSettings["FeedbackToBuyers"].Split('\n').Select(x => x.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

			var getItemsAwaitingFeedback = new GetItemsAwaitingFeedbackCall(apiContext);

			PaginationType paginationType = new PaginationType();
			paginationType.EntriesPerPage = 100;
			getItemsAwaitingFeedback.Pagination = paginationType;
			PaginatedTransactionArrayType awaitingFeedbackItems = null;

			PaginationType paginationTypeTrans = new PaginationType()
			{
				EntriesPerPage = 100
			};

			int itemCount = 0;
			paginationType.PageNumber = 1;
			Random random = new Random();
			var uniqueItemIdsFound = new HashSet<string>();
			do
			{
				//if (generalStatusUpdate != null)
				//{
				//	generalStatusUpdate(baseMessage + baseMessageGettingListOfItems + " Page "
				//		+ paginationType.PageNumber.ToString()
				//		+ (awaitingFeedbackItems == null ? string.Empty
				//			: (" of " + awaitingFeedbackItems.PaginationResult.TotalNumberOfPages.ToString())), null);
				//}
				awaitingFeedbackItems = getItemsAwaitingFeedback.GetItemsAwaitingFeedback(ItemSortTypeCodeType.EndTime, paginationType);
				//if (generalStatusUpdate != null)
				//{
				//	int percentComplete = (((((paginationType.PageNumber - 1) * paginationType.EntriesPerPage) + awaitingFeedbackItems.TransactionArray.Count) * 100) / awaitingFeedbackItems.PaginationResult.TotalNumberOfEntries) / 2;
				//	generalStatusUpdate(null, percentComplete);
				//}

				// process returned transaction data for the current page     
				Parallel.ForEach((TransactionType[])awaitingFeedbackItems.TransactionArray.ToArray(),
					new ParallelOptions { MaxDegreeOfParallelism = 8 },
					(awaitingFeedbackItem) =>
				{
					try
					{
						if (awaitingFeedbackItem.Item == null)
							return;	// no item data

						uniqueItemIdsFound.Add(awaitingFeedbackItem.Item.ItemID);	// A Hashset will only keep a single instance of unique values

						var itemSummary = GetItemSummary(awaitingFeedbackItem, apiContext);
						//allAwaitingFeedbackItems.Add(awaitingFeedbackItem);
						if (itemSummary == null)
							return;

						each_ProfileName_Item(profileName, itemSummary);
						//var thread = new Thread(new ParameterizedThreadStart(parallel_each_ProfileName_Item));
						//thread.Start(new object[] { each_ProfileName_Item, profileName, itemDetails, awaitingFeedbackItem });

						//if (feedbackUpdate != null)
						//{
						//	var updates = new FeedbackUpdates()
						//	{
						//		Seller = awaitingFeedbackItem.Item.Seller.UserID,
						//		Title = awaitingFeedbackItem.Item.Title,
						//		Status = "Pending"
						//	};

						//	feedbackUpdate(awaitingFeedbackItem.Item.ItemID, updates);
						//}
					}
					catch (Exception ex)
					{
					}
				});
				
				//uniqueItemIdsFound.Clear();
			} while (++paginationType.PageNumber <= awaitingFeedbackItems.PaginationResult.TotalNumberOfPages);

			return itemCount;
		}

		private EbayItemSummary GetItemSummary(TransactionType transactionDetails, ApiContext apiContext)
		{
			EbayItemSummary itemSummary = null;

			itemSummary = GetCachedItemSummary(transactionDetails.Item.ItemID, transactionDetails.TransactionID);
			if (itemSummary != null)
				return itemSummary;

			try
			{
				var getItemCall = new eBay.Service.Call.GetItemCall(apiContext) { TransactionID = transactionDetails.TransactionID };
				var itemDetails = getItemCall.GetItem(transactionDetails.Item.ItemID);

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
						ItemId = itemDetails.ItemID,
						TransactionId = transactionDetails.TransactionID,
						Seller = transactionDetails.Item.Seller.UserID,
						GalleryImageUrl = itemDetails.PictureDetails.GalleryURL,
						GalleryImage = image,
						Title = itemDetails.Title,
						EndDateTime = itemDetails.ListingDetails.EndTime
					};

					SetCachedItemSummary(itemSummary);
				}
				catch (Exception ex)
				{
				}
			}
			catch (Exception ex)
			{ 
			}

			return itemSummary;
		}

		private object _dbLock = new object();
		private SQLiteDatabase _db;
		public SQLiteDatabase DB
		{
			get
			{
				lock (_dbLock)
				{
					if (_db == null)
					{
						_db = new SQLiteDatabase(ConfigurationManager.AppSettings["SqliteDataFile"]);
						VerifyDb();
					}

					return _db;
				}
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
						[Title] TEXT NOT NULL,
						[Seller] TEXT NULL,
						[GalleryImageUrl] TEXT NULL,
						[GalleryImage] BLOB NULL,
						[EndDateTime] TIMESTAMP NOT NULL,
						[CreateDateTime] TIMESTAMP NOT NULL,
						[Status] TEXT NULL,
						[FeedbackLeft] TEXT NULL
					);

					CREATE UNIQUE INDEX [IDX_EbayItemsAwaitingFeedback_ItemId] ON [EbayItemsAwaitingFeedback](
						[ItemId] ASC
					);
					";

				_db.ExecuteNonQuery(createSql);
			}
		}


		private EbayItemSummary GetCachedItemSummary(string itemId, string transactionId = null)
		{
			string sql = "SELECT ItemId, Title, Seller, GalleryImage, EndDateTime FROM EbayItemsAwaitingFeedback WHERE ItemId = '" 
				+ itemId.Replace("'", "") + "'"
				+ (transactionId == null ? string.Empty : " AND TransactionId = '" + transactionId.Replace("'", "")) + "'";
			var dataColumns = new DataColumn[]
			{
				new DataColumn("ItemId", typeof(string)),
				new DataColumn("Title", typeof(string)),
				new DataColumn("Seller", typeof(string)),
				new DataColumn("GalleryImage", typeof(byte[])),
				new DataColumn("EndDateTime", typeof(DateTime))
			};
			DataRow dataRow;
			lock (_dbLock)
			{
				dataRow = DB.GetDataRow(sql, dataColumns);
			}
			if (dataRow == null)
				return null;

			Bitmap galleryImage = null;
			var galleryImageBytes = dataRow.ItemArray[3] as byte[];
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
				}
			}

			EbayItemSummary itemSummary = new EbayItemSummary()
			{
				ItemId				= dataRow.ItemArray[0] as string,
				Title				= dataRow.ItemArray[1] as string,
				Seller				= dataRow.ItemArray[2] as string,
				GalleryImage		= galleryImage,
				EndDateTime			= (DateTime) dataRow.ItemArray[4],
			};

			return itemSummary;
		}

		private int SetCachedItemSummary(EbayItemSummary itemSummary)
		{
			var data = new Dictionary<string, object>();
			data["ItemId"] = itemSummary.ItemId;
			data["TransactionId"] = itemSummary.TransactionId;
			data["Title"] = itemSummary.Title;
			data["Seller"] = itemSummary.Seller;
			data["GalleryImageUrl"] = itemSummary.GalleryImageUrl;
			data["GalleryImage"] = (itemSummary.GalleryImage == null ? null : (byte[])new ImageConverter().ConvertTo(itemSummary.GalleryImage, typeof(byte[])));
			data["EndDateTime"] = itemSummary.EndDateTime;
			data["CreateDateTime"] = DateTime.Now;

			lock (_dbLock)
			{
				return DB.Insert("EbayItemsAwaitingFeedback", data);
			}
		}

		private void parallel_each_ProfileName_Item(object obj)
		{
			var objects = (object[])obj;
			var each_ProfileName_Item = (Action<string, ItemType>)objects[0];
			var profileName = (string)objects[1];
			var itemDetails = (ItemType)objects[2];

			each_ProfileName_Item(profileName, itemDetails);
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
			apiLogManager.ApiLoggerList.Add(new FileLogger("ebayApiLog.txt", true, true, true));

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
