using System.Windows.Forms;

namespace EbayLeaveBulkFeedback
{
	public static class Prompt
	{
		public static string ShowDialog(string text, string caption, string defaultValue = null)
		{
			Form prompt = new Form();
			prompt.Width = 545;
			prompt.Height = 150;
			prompt.FormBorderStyle = FormBorderStyle.Sizable;
			prompt.Text = caption;
			prompt.StartPosition = FormStartPosition.CenterScreen;
			Label textLabel = new Label() { Left = 10, Top = 10, Width = 550, Text = text };
			TextBox textBox = new TextBox() { Left = 10, Top = 30, Width = 500, Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, WordWrap = true, Multiline = true, Text = defaultValue };
			Button confirmation = new Button() { Text = "Ok", Left = 410, Width = 100, Top = 60, Height = 30, Anchor = AnchorStyles.Bottom | AnchorStyles.Right, DialogResult = DialogResult.OK };
			confirmation.Click += (sender, e) => { prompt.Close(); };
			prompt.Controls.Add(textBox);
			prompt.Controls.Add(confirmation);
			prompt.Controls.Add(textLabel);
			prompt.AcceptButton = confirmation;

			return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : null;
		}
	}
}


