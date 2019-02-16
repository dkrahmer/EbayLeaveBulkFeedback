using System;

namespace EbayLeaveBulkFeedback
{
	public class EbayItemSummary
	{
		public string ItemId { get; set; }
		public string TransactionId { get; set; }
		public string GalleryImageUrl { get; set; }
		public string Title { get; set; }
		public decimal Price { get; set; }
		public DateTime EndDateTime { get; set; }
		//public System.Drawing.Bitmap GalleryImage { get; set; }

		//byte[] imageBytes = null;
		//imageBytes = (byte[])new ImageConverter().ConvertTo(image, typeof(byte[]));


		public string Seller { get; set; }

		public string ProfileName { get; set; }

		public string OrderLineItemId { get; set; }

		public string TrackingNumber { get; set; }
	}
}
