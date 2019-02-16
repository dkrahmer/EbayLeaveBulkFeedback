namespace EbayLeaveBulkFeedback
{
	partial class ConfigDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxAuctionDataFile = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.buttonAddUserToken = new System.Windows.Forms.Button();
			this.buttonRemoveUserToken = new System.Windows.Forms.Button();
			this.listViewUserTokens = new System.Windows.Forms.ListView();
			this.EbayUserToken = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.buttonRequestToken = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.buttonAddFeedbackToSeller = new System.Windows.Forms.Button();
			this.buttonRemoveFeedbackToSeller = new System.Windows.Forms.Button();
			this.listViewFeedbackToSellers = new System.Windows.Forms.ListView();
			this.Feedback = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonEditUserToken = new System.Windows.Forms.Button();
			this.buttonEditFeedbackToSeller = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(115, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Auction Data File";
			// 
			// textBoxAuctionDataFile
			// 
			this.textBoxAuctionDataFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxAuctionDataFile.Location = new System.Drawing.Point(153, 6);
			this.textBoxAuctionDataFile.Name = "textBoxAuctionDataFile";
			this.textBoxAuctionDataFile.Size = new System.Drawing.Size(620, 22);
			this.textBoxAuctionDataFile.TabIndex = 1;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.buttonEditUserToken);
			this.groupBox1.Controls.Add(this.buttonAddUserToken);
			this.groupBox1.Controls.Add(this.buttonRemoveUserToken);
			this.groupBox1.Controls.Add(this.listViewUserTokens);
			this.groupBox1.Controls.Add(this.buttonRequestToken);
			this.groupBox1.Location = new System.Drawing.Point(6, 34);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(767, 500);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Ebay User Tokens";
			// 
			// buttonAddUserToken
			// 
			this.buttonAddUserToken.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAddUserToken.Location = new System.Drawing.Point(286, 443);
			this.buttonAddUserToken.Name = "buttonAddUserToken";
			this.buttonAddUserToken.Size = new System.Drawing.Size(134, 42);
			this.buttonAddUserToken.TabIndex = 19;
			this.buttonAddUserToken.Text = "Add...";
			this.buttonAddUserToken.UseVisualStyleBackColor = true;
			this.buttonAddUserToken.Click += new System.EventHandler(this.ButtonAddUserToken_Click);
			// 
			// buttonRemoveUserToken
			// 
			this.buttonRemoveUserToken.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonRemoveUserToken.Location = new System.Drawing.Point(6, 443);
			this.buttonRemoveUserToken.Name = "buttonRemoveUserToken";
			this.buttonRemoveUserToken.Size = new System.Drawing.Size(134, 42);
			this.buttonRemoveUserToken.TabIndex = 18;
			this.buttonRemoveUserToken.Text = "Remove Selected";
			this.buttonRemoveUserToken.UseVisualStyleBackColor = true;
			this.buttonRemoveUserToken.Click += new System.EventHandler(this.ButtonRemoveUserToken_Click);
			// 
			// listViewUserTokens
			// 
			this.listViewUserTokens.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listViewUserTokens.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.EbayUserToken});
			this.listViewUserTokens.FullRowSelect = true;
			this.listViewUserTokens.LabelEdit = true;
			this.listViewUserTokens.Location = new System.Drawing.Point(6, 21);
			this.listViewUserTokens.Name = "listViewUserTokens";
			this.listViewUserTokens.Size = new System.Drawing.Size(755, 410);
			this.listViewUserTokens.TabIndex = 8;
			this.listViewUserTokens.UseCompatibleStateImageBehavior = false;
			this.listViewUserTokens.View = System.Windows.Forms.View.Details;
			this.listViewUserTokens.DoubleClick += new System.EventHandler(this.ListViewUserTokens_DoubleClick);
			// 
			// EbayUserToken
			// 
			this.EbayUserToken.Text = "eBay User Token";
			this.EbayUserToken.Width = 750;
			// 
			// buttonRequestToken
			// 
			this.buttonRequestToken.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonRequestToken.Location = new System.Drawing.Point(426, 443);
			this.buttonRequestToken.Name = "buttonRequestToken";
			this.buttonRequestToken.Size = new System.Drawing.Size(134, 42);
			this.buttonRequestToken.TabIndex = 17;
			this.buttonRequestToken.Text = "Request Token...";
			this.buttonRequestToken.UseVisualStyleBackColor = true;
			this.buttonRequestToken.Click += new System.EventHandler(this.ButtonRequestToken_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.buttonEditFeedbackToSeller);
			this.groupBox2.Controls.Add(this.buttonAddFeedbackToSeller);
			this.groupBox2.Controls.Add(this.buttonRemoveFeedbackToSeller);
			this.groupBox2.Controls.Add(this.listViewFeedbackToSellers);
			this.groupBox2.Location = new System.Drawing.Point(6, 6);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(856, 528);
			this.groupBox2.TabIndex = 8;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Feedback To Sellers";
			// 
			// buttonAddFeedbackToSeller
			// 
			this.buttonAddFeedbackToSeller.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAddFeedbackToSeller.Location = new System.Drawing.Point(286, 471);
			this.buttonAddFeedbackToSeller.Name = "buttonAddFeedbackToSeller";
			this.buttonAddFeedbackToSeller.Size = new System.Drawing.Size(134, 42);
			this.buttonAddFeedbackToSeller.TabIndex = 19;
			this.buttonAddFeedbackToSeller.Text = "Add...";
			this.buttonAddFeedbackToSeller.UseVisualStyleBackColor = true;
			this.buttonAddFeedbackToSeller.Click += new System.EventHandler(this.ButtonAddFeedbackToSeller_Click);
			// 
			// buttonRemoveFeedbackToSeller
			// 
			this.buttonRemoveFeedbackToSeller.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonRemoveFeedbackToSeller.Location = new System.Drawing.Point(6, 471);
			this.buttonRemoveFeedbackToSeller.Name = "buttonRemoveFeedbackToSeller";
			this.buttonRemoveFeedbackToSeller.Size = new System.Drawing.Size(134, 42);
			this.buttonRemoveFeedbackToSeller.TabIndex = 18;
			this.buttonRemoveFeedbackToSeller.Text = "Remove Selected";
			this.buttonRemoveFeedbackToSeller.UseVisualStyleBackColor = true;
			this.buttonRemoveFeedbackToSeller.Click += new System.EventHandler(this.ButtonRemoveFeedbackToSeller_Click);
			// 
			// listViewFeedbackToSellers
			// 
			this.listViewFeedbackToSellers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listViewFeedbackToSellers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Feedback});
			this.listViewFeedbackToSellers.FullRowSelect = true;
			this.listViewFeedbackToSellers.LabelEdit = true;
			this.listViewFeedbackToSellers.Location = new System.Drawing.Point(6, 21);
			this.listViewFeedbackToSellers.Name = "listViewFeedbackToSellers";
			this.listViewFeedbackToSellers.Size = new System.Drawing.Size(844, 438);
			this.listViewFeedbackToSellers.TabIndex = 8;
			this.listViewFeedbackToSellers.UseCompatibleStateImageBehavior = false;
			this.listViewFeedbackToSellers.View = System.Windows.Forms.View.Details;
			this.listViewFeedbackToSellers.DoubleClick += new System.EventHandler(this.ListViewFeedbackToSellers_DoubleClick);
			// 
			// Feedback
			// 
			this.Feedback.Text = "Feedback";
			this.Feedback.Width = 650;
			// 
			// buttonSave
			// 
			this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSave.Location = new System.Drawing.Point(525, 587);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(134, 42);
			this.buttonSave.TabIndex = 20;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.Location = new System.Drawing.Point(665, 587);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(134, 42);
			this.buttonCancel.TabIndex = 21;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
			// 
			// buttonEditUserToken
			// 
			this.buttonEditUserToken.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonEditUserToken.Location = new System.Drawing.Point(146, 443);
			this.buttonEditUserToken.Name = "buttonEditUserToken";
			this.buttonEditUserToken.Size = new System.Drawing.Size(134, 42);
			this.buttonEditUserToken.TabIndex = 20;
			this.buttonEditUserToken.Text = "Edit Selected...";
			this.buttonEditUserToken.UseVisualStyleBackColor = true;
			this.buttonEditUserToken.Click += new System.EventHandler(this.ButtonEditUserToken_Click);
			// 
			// buttonEditFeedbackToSeller
			// 
			this.buttonEditFeedbackToSeller.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonEditFeedbackToSeller.Location = new System.Drawing.Point(146, 471);
			this.buttonEditFeedbackToSeller.Name = "buttonEditFeedbackToSeller";
			this.buttonEditFeedbackToSeller.Size = new System.Drawing.Size(134, 42);
			this.buttonEditFeedbackToSeller.TabIndex = 21;
			this.buttonEditFeedbackToSeller.Text = "Edit Selected...";
			this.buttonEditFeedbackToSeller.UseVisualStyleBackColor = true;
			this.buttonEditFeedbackToSeller.Click += new System.EventHandler(this.ButtonEditFeedbackToSeller_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(787, 569);
			this.tabControl1.TabIndex = 22;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.textBoxAuctionDataFile);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(779, 540);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "General";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.groupBox2);
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(868, 540);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Feedback";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// ConfigDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(811, 639);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonSave);
			this.MinimumSize = new System.Drawing.Size(530, 500);
			this.Name = "ConfigDialog";
			this.Text = "Config";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxAuctionDataFile;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button buttonAddUserToken;
		private System.Windows.Forms.Button buttonRemoveUserToken;
		private System.Windows.Forms.ListView listViewUserTokens;
		private System.Windows.Forms.Button buttonRequestToken;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button buttonAddFeedbackToSeller;
		private System.Windows.Forms.Button buttonRemoveFeedbackToSeller;
		private System.Windows.Forms.ListView listViewFeedbackToSellers;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.ColumnHeader EbayUserToken;
		private System.Windows.Forms.ColumnHeader Feedback;
		private System.Windows.Forms.Button buttonEditUserToken;
		private System.Windows.Forms.Button buttonEditFeedbackToSeller;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
	}
}