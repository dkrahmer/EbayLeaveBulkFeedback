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

namespace ebayLeaveFeedbackForSellers
{
	class EbayLeaveFeedback
	{
		private ApiContext _apiContext = null;

		public EbayLeaveFeedback()
		{
			// Last page of waiting for feedback: http://www.ebay.com/myb/PurchaseHistory#PurchaseHistoryOrdersContainer?ipp=100&Period=1&Filter=9&radioChk=1&GotoPage=100&_trksid=p2057872.m2749.l4670&cmid=2749&melid=page&_trksid=p2057872.m2749.l4670
			// Get AppID and ServerAddress from Web.config
			string appID = ConfigurationManager.AppSettings["EbayAppId"];
			string ebayFindingServiceUrl = ConfigurationManager.AppSettings["EbayFindingServiceUrl"];

			ClientConfig config = new ClientConfig();
			// Initialize service end-point configration
			config.EndPointAddress = ebayFindingServiceUrl;

			// set eBay developer account AppID
			config.ApplicationId = appID;

			// Create a service client
			_apiContext = GetApiContext();
		}

		public int LeaveFeedbacks(HashSet<string> itemIds, Action<string, int> generalStatusUpdate = null, Action<string, FeedbackUpdates> feedbackUpdate = null)
		{
			const string baseMessageGettingListOfItems = "Getting list of item transactions that need feedback...";
			if (generalStatusUpdate != null)
				generalStatusUpdate(baseMessageGettingListOfItems, 0);

			var feedbackToSellers = ConfigurationManager.AppSettings["FeedbackToSellers"].Split('\n').Select(x => x.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
			var feedbackToBuyers = ConfigurationManager.AppSettings["FeedbackToBuyers"].Split('\n').Select(x => x.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

			var getItemsAwaitingFeedback = new GetItemsAwaitingFeedbackCall(_apiContext);

			PaginationType paginationType = new PaginationType();
			paginationType.EntriesPerPage = 100;
			getItemsAwaitingFeedback.Pagination = paginationType;
			PaginatedTransactionArrayType awaitingFeedbackItems;

			PaginationType paginationTypeTrans = new PaginationType()
			{
				EntriesPerPage = 100
			};

			int feedbackCount = 0;
			var allAwaitingFeedbackItems = new List<TransactionType>();
			paginationType.PageNumber = 1;
			Random random = new Random();
			var uniqueItemIdsFound = new HashSet<string>();
			do
			{
				awaitingFeedbackItems = getItemsAwaitingFeedback.GetItemsAwaitingFeedback(ItemSortTypeCodeType.EndTime, paginationType);
				if (generalStatusUpdate != null)
				{
					int percentComplete = (((((paginationType.PageNumber - 1) * paginationType.EntriesPerPage) + awaitingFeedbackItems.TransactionArray.Count) * 100) / awaitingFeedbackItems.PaginationResult.TotalNumberOfEntries) / 2;
					generalStatusUpdate(baseMessageGettingListOfItems + " Page " + paginationType.PageNumber.ToString() + " of " + awaitingFeedbackItems.PaginationResult.TotalNumberOfPages.ToString(), percentComplete);
				}

				// process returned transaction data for the current page     
				for (var awaitingFeedbackItemNumber = 0; awaitingFeedbackItemNumber < awaitingFeedbackItems.TransactionArray.Count; awaitingFeedbackItemNumber++)
				{
					var awaitingFeedbackItem = awaitingFeedbackItems.TransactionArray[awaitingFeedbackItemNumber];

					if (awaitingFeedbackItem.Item == null)
						continue;	// no item data

					if (!itemIds.Contains(awaitingFeedbackItem.Item.ItemID))
						continue;

					uniqueItemIdsFound.Add(awaitingFeedbackItem.Item.ItemID);	// A Hashset will only keep a single instance of unique values

					allAwaitingFeedbackItems.Add(awaitingFeedbackItem);

					if (feedbackUpdate != null)
					{
						var updates = new FeedbackUpdates()
						{
							Seller = awaitingFeedbackItem.Item.Seller.UserID,
							Title = awaitingFeedbackItem.Item.Title,
							Status = "Pending"
						};

						feedbackUpdate(awaitingFeedbackItem.Item.ItemID, updates);
					}
				}
			} while (++paginationType.PageNumber <= awaitingFeedbackItems.PaginationResult.TotalNumberOfPages
						&& uniqueItemIdsFound.Count < itemIds.Count);	// Stop reading data if 

			int feedbackItemNumber = 0;
			foreach (var feedbackItem in allAwaitingFeedbackItems)
			{
				string giveFeedbackTo = null;
				if (feedbackItem.Item.Seller != null)
				{
					giveFeedbackTo = feedbackItem.Item.Seller.UserID;
				}
				else if (feedbackItem.Buyer != null)
				{
					giveFeedbackTo = feedbackItem.Buyer.UserID;
				}
				else
				{
					// no buyer or seller info? That's wierd, nothing to do here...
					continue;
				}

				if (generalStatusUpdate != null)
				{
					int percentComplete = 50 + (((feedbackItemNumber * 100) / allAwaitingFeedbackItems.Count) / 2);
					generalStatusUpdate("Giving feedback to [" + giveFeedbackTo + "] for item: " + feedbackItem.Item.Title + " (" + feedbackItem.Item.ItemID + ")", percentComplete);
				}
				var itemRatingDetailsTypeCollection = new ItemRatingDetailsTypeCollection();
				itemRatingDetailsTypeCollection.Add(new ItemRatingDetailsType() { Rating = 5, RatingDetail = FeedbackRatingDetailCodeType.Communication });
				itemRatingDetailsTypeCollection.Add(new ItemRatingDetailsType() { Rating = 5, RatingDetail = FeedbackRatingDetailCodeType.ItemAsDescribed });
				itemRatingDetailsTypeCollection.Add(new ItemRatingDetailsType() { Rating = 5, RatingDetail = FeedbackRatingDetailCodeType.ShippingAndHandlingCharges });
				itemRatingDetailsTypeCollection.Add(new ItemRatingDetailsType() { Rating = 5, RatingDetail = FeedbackRatingDetailCodeType.ShippingTime });

				var leaveFeedbackCall = new LeaveFeedbackCall(_apiContext);

				string feedback = null;

				if (feedbackItem.Buyer == null)
				{
					// we are the buyer
					feedback = feedbackToSellers[random.Next(0, feedbackToSellers.Length - 1)];
				}
				else
				{
					feedback = feedbackToBuyers[random.Next(0, feedbackToBuyers.Length - 1)];
				}

				try
				{
					string result = leaveFeedbackCall.LeaveFeedback(
						feedbackItem.Item.ItemID,
						feedback,
						CommentTypeCodeType.Positive,
						feedbackItem.TransactionID,
						giveFeedbackTo,
						itemRatingDetailsTypeCollection,
						feedbackItem.OrderLineItemID,
						ItemArrivedWithinEDDCodeType.BuyerIndicatedItemArrivedWithinEDDRange);	// eBay.Service.Core.Soap

					if (feedbackUpdate != null)
					{
						var updates = new FeedbackUpdates()
						{
							FeedbackLeft = feedback,
							Status = "Done",
							Result = result
						};
						feedbackUpdate(feedbackItem.Item.ItemID, updates);
					}
					feedbackCount++;
				}
				catch (Exception ex)
				{

				}

				feedbackItemNumber++;
			}
			
			return feedbackCount;
		}

		public bool LeaveFeedback(string itemId, string commentText, string transactionID, string targetUser)
		{
			var request = new LeaveFeedbackCall(_apiContext);
			request.ItemID = itemId;
			request.TargetUser = targetUser;

			request.LeaveFeedback(itemId, commentText, CommentTypeCodeType.Positive, transactionID, targetUser);
			return true;
		}

		/// <summary>
		/// Populate eBay SDK ApiContext object with data from application configuration file
		/// </summary>
		/// <returns>ApiContext object</returns>
		ApiContext GetApiContext()
		{
			//apiContext is a singleton,
			//to avoid duplicate configuration reading
			if (_apiContext != null)
			{
				return _apiContext;
			}
			else
			{
				_apiContext = new ApiContext();

				//set Api Server Url
				_apiContext.SoapApiServerUrl = ConfigurationManager.AppSettings["EbayApiServerUrl"];
				//set Api Token to access eBay Api Server
				ApiCredential apiCredential = new ApiCredential();
				apiCredential.eBayToken = ConfigurationManager.AppSettings["EbayUserToken"];
				_apiContext.ApiCredential = apiCredential;
				//set eBay Site target to US
				_apiContext.Site = SiteCodeType.US;

				//set Api logging
				_apiContext.ApiLogManager = new ApiLogManager();
				_apiContext.ApiLogManager.ApiLoggerList.Add(
					new FileLogger("ebayApiLog.txt", true, true, true)
					);
				_apiContext.ApiLogManager.EnableLogging = true;

				return _apiContext;
			}
		}
	}
}
