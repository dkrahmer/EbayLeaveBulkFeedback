using System;
using System.Collections;
using System.Windows.Forms;

namespace EbayLeaveBulkFeedback
{
	public class ListViewItemComparer : IComparer
	{
		private readonly int col;
		public ListViewItemComparer()
		{
			col = 0;
		}
		public ListViewItemComparer(int column)
		{
			col = column;
		}
		public int Compare(object x, object y)
		{
			int returnVal = -1;
			returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
			((ListViewItem)y).SubItems[col].Text);
			return returnVal;
		}
	}
}
