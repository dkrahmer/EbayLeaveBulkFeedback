using System;
using System.Windows.Forms;

namespace EbayLeaveBulkFeedback
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			var dataManager = new DataManager();
			Application.Run(new MainForm(dataManager));
		}
	}
}
