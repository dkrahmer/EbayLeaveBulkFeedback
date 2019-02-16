using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using eBay.Service.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EbayLeaveBulkFeedback
{
	public class AvailableFeedbackItemProcessor
	{
		private readonly SQLiteDatabase _db;

		public AvailableFeedbackItemProcessor(SQLiteDatabase db)
		{
			_db = db;
		}

		public int GetAvailableFeedbackItems(Action<string, string, string, string> each_ItemId_TransactionId_OrderLineItemId_ProfileName)
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
				totalItems += GetAvailableFeedbackItems(apiContext, each_ItemId_TransactionId_OrderLineItemId_ProfileName, profileName: i.ToString());
			}

			return totalItems;
		}


		public int GetAvailableFeedbackItems(ApiContext apiContext, Action<string, string, string, string> each_ItemId_TransactionId_OrderLineItemId_ProfileName, string profileName)
		{
			string baseMessage = (profileName == null ? string.Empty : "Profile " + profileName + ": ");
			//string baseMessageGettingListOfItems = "Getting list of item transactions that need feedback...";
			//if (generalStatusUpdate != null)
			//	generalStatusUpdate(baseMessage + baseMessageGettingListOfItems, 0);

			var feedbackToSellers = ConfigurationManager.AppSettings["FeedbackToSellers"].Split('\n').Select(x => x.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
			var feedbackToBuyers = ConfigurationManager.AppSettings["FeedbackToBuyers"].Split('\n').Select(x => x.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

			var getItemsAwaitingFeedback = new GetItemsAwaitingFeedbackCall(apiContext);

			PaginationType paginationType = new PaginationType
			{
				EntriesPerPage = 100
			};
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
							return; // no item data

						uniqueItemIdsFound.Add(awaitingFeedbackItem.Item.ItemID);   // A Hashset will only keep a single instance of unique values

						//var itemSummary = GetItemSummary(awaitingFeedbackItem, apiContext);
						//allAwaitingFeedbackItems.Add(awaitingFeedbackItem);

						each_ItemId_TransactionId_OrderLineItemId_ProfileName(awaitingFeedbackItem.Item.ItemID, awaitingFeedbackItem.OrderLineItemID, awaitingFeedbackItem.TransactionID, profileName);
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
					catch //(Exception ex)
					{
					}
				});

				//uniqueItemIdsFound.Clear();
			} while (++paginationType.PageNumber <= awaitingFeedbackItems.PaginationResult.TotalNumberOfPages);

			return itemCount;
		}


		/// <summary>
		/// Populate eBay SDK ApiContext object with data from application configuration file
		/// </summary>
		/// <returns>ApiContext object</returns>
		private ApiContext GetApiContext(string ebayUserToken)
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
