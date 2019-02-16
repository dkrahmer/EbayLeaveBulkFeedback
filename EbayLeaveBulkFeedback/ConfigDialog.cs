using System;
using System.Configuration;
using System.Windows.Forms;

namespace EbayLeaveBulkFeedback
{
	public partial class ConfigDialog : Form
	{
		private DataManager _dataManager;

		public ConfigDialog(DataManager dataManager)
		{
			_dataManager = dataManager;
			InitializeComponent();
			LoadFromConfig();
		}

		private void LoadFromConfig()
		{
			for (int i = 0; ; i++)
			{
				string ebayUserToken = ConfigurationManager.AppSettings["EbayUserToken" + (i == 0 ? string.Empty : i.ToString())];
				if (string.IsNullOrEmpty(ebayUserToken))
				{
					if (i <= 1)
						continue;
					else
						break;
				}

				AddUserTokenToList(ebayUserToken);
			}

			for (int i = 0; ; i++)
			{
				string feedbackToSellers = ConfigurationManager.AppSettings["FeedbackToSellers" + (i == 0 ? string.Empty : (i + 1).ToString("000"))];
				if (string.IsNullOrEmpty(feedbackToSellers))
				{
					if (i == 0)
						continue;
					else
						break;
				}

				if (i == 0)
				{
					var feedbacks = feedbackToSellers.Split('\n');
					foreach (var feedback in feedbacks)
					{
						AddFeedbackToSellerToList(feedback.Trim());
					}
				}
				else
				{
					AddFeedbackToSellerToList(feedbackToSellers);
				}
			}
		}

		private void AddFeedbackToSellerToList(string feedback)
		{
			var listViewItem = new ListViewItem(feedback);
			listViewFeedbackToSellers.Items.Add(listViewItem);
		}

		private void ButtonAddUserToken_Click(object sender, EventArgs e)
		{
			string newUserToken = Prompt.ShowDialog("Add eBay User Token", "Add eBay User Token");
			if (!string.IsNullOrEmpty(newUserToken))
				AddUserTokenToList(newUserToken);
		}

		private void AddUserTokenToList(string newUserToken)
		{
			var listViewItem = new ListViewItem(newUserToken);
			listViewUserTokens.Items.Add(listViewItem);
		}

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ButtonSave_Click(object sender, EventArgs e)
		{
			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			int i = 0;
			bool settingExists;
			bool listViewItemExists;
			do
			{
				string settingName = "EbayUserToken" + (i == 0 ? string.Empty : (i + 1).ToString());
				var ebayUserToken = ConfigurationManager.AppSettings[settingName];
				settingExists = false;
				listViewItemExists = false;
				if (ebayUserToken != null)
				{
					settingExists = true;
					config.AppSettings.Settings.Remove(settingName);
				}

				if (i < listViewUserTokens.Items.Count)
				{
					listViewItemExists = true;
					ListViewItem listViewItem = listViewUserTokens.Items[i];
					config.AppSettings.Settings.Add(settingName, listViewItem.Text);
				}

				i++;
			} while (settingExists || listViewItemExists);

			do
			{
				string settingName = "FeedbackToSellers" + (i + 1).ToString("000");
				var ebayUserToken = ConfigurationManager.AppSettings[settingName];
				settingExists = false;
				listViewItemExists = false;
				if (ebayUserToken != null)
				{
					settingExists = true;
					config.AppSettings.Settings.Remove(settingName);
				}

				if (i < listViewUserTokens.Items.Count)
				{
					listViewItemExists = true;
					ListViewItem listViewItem = listViewUserTokens.Items[i];
					config.AppSettings.Settings.Add(settingName, listViewItem.Text);
				}

				i++;
			} while (settingExists || listViewItemExists);

			config.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection("appSettings");
			this.Close();
		}

		private void ButtonRemoveUserToken_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem selectedItem in listViewUserTokens.SelectedItems)
			{
				listViewUserTokens.Items.Remove(selectedItem);
			}
		}

		private void ButtonRemoveFeedbackToSeller_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem selectedItem in listViewFeedbackToSellers.SelectedItems)
			{
				listViewFeedbackToSellers.Items.Remove(selectedItem);
			}
		}

		private void ButtonAddFeedbackToSeller_Click(object sender, EventArgs e)
		{
			string newFeedbackToSellers = Prompt.ShowDialog("Add Feedback To Sellers", "Add Feedback To Sellers");
			if (!string.IsNullOrEmpty(newFeedbackToSellers))
				AddFeedbackToSellersToList(newFeedbackToSellers);
		}

		private void AddFeedbackToSellersToList(string newFeedbackToSellers)
		{
			var listViewItem = new ListViewItem(newFeedbackToSellers);
			listViewFeedbackToSellers.Items.Add(listViewItem);
		}

		private void ButtonEditUserToken_Click(object sender, EventArgs e)
		{
			EditSelectedUserTokens();
		}
		private void EditSelectedUserTokens()
		{
			foreach (ListViewItem listViewItem in listViewUserTokens.SelectedItems)
			{
				string userToken = Prompt.ShowDialog("Edit eBay User Token", "Edit eBay User Token", listViewItem.Text);
				if (string.IsNullOrEmpty(userToken))
					break;
				listViewItem.Text = userToken;
			}
		}

		private void ListViewUserTokens_DoubleClick(object sender, EventArgs e)
		{
			EditSelectedUserTokens();
		}

		private void ButtonEditFeedbackToSeller_Click(object sender, EventArgs e)
		{
			EditSelectedFeedbackToSellers();
		}

		private void EditSelectedFeedbackToSellers()
		{
			foreach (ListViewItem listViewItem in listViewFeedbackToSellers.SelectedItems)
			{
				string feedback = Prompt.ShowDialog("Edit Feedback To Sellers", "Edit Feedback To Sellers", listViewItem.Text);
				if (string.IsNullOrEmpty(feedback))
					break;

				listViewItem.Text = feedback;
			}
		}

		private void ListViewFeedbackToSellers_DoubleClick(object sender, EventArgs e)
		{
			EditSelectedFeedbackToSellers();
		}

		private void ButtonRequestToken_Click(object sender, EventArgs e)
		{
			var sessionId = _dataManager.GetEbaySessionId();
		}
	}
}
