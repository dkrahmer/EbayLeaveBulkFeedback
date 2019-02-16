using System.Windows.Forms;

namespace EbayLeaveBulkFeedback
{
	public class ListViewNonFlicker : ListView
	{
		public ListViewNonFlicker()
			: base()
		{
			DoubleBuffered = true;
		}

		public event ScrollEventHandler Scroll;
		protected virtual void OnScroll(ScrollEventArgs e)
		{
			Scroll?.Invoke(this, e);
		}

		private const int WM_HSCROLL = 0x114;
		private const int WM_VSCROLL = 0x115;
		private const int WM_MOUSEWHEEL = 0x020A;
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			if (m.Msg == WM_HSCROLL || m.Msg == WM_VSCROLL || m.Msg == WM_MOUSEWHEEL)
			{
				OnScroll(new ScrollEventArgs((ScrollEventType)(m.WParam.ToInt32() & 0xffff), 0));
			}
		}
	}
}
