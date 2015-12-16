using EbayLeaveBulkFeedback;
namespace EbayLeaveBulkFeedback
{
	partial class MainForm
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
			this.panelMain = new System.Windows.Forms.Panel();
			this.splitterRawParsed = new System.Windows.Forms.Splitter();
			this.feedbackListView = new EbayLeaveBulkFeedback.ListViewNonFlicker();
			this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ItemId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.SellerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.FeedbackLeft = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Result = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.TransactionId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.OrderLineItemId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ProfileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.textBoxRawData = new System.Windows.Forms.RichTextBox();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.buttonLeaveFeedback = new System.Windows.Forms.Button();
			this.buttonStop = new System.Windows.Forms.Button();
			this.toolStripItemCount = new System.Windows.Forms.Label();
			this.buttonSanitizeList = new System.Windows.Forms.Button();
			this.buttonClearCompleted = new System.Windows.Forms.Button();
			this.buttonItemPicker = new System.Windows.Forms.Button();
			this.buttonIgnoreSelected = new System.Windows.Forms.Button();
			this.panelMain.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelMain
			// 
			this.panelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelMain.Controls.Add(this.splitterRawParsed);
			this.panelMain.Controls.Add(this.feedbackListView);
			this.panelMain.Controls.Add(this.textBoxRawData);
			this.panelMain.Location = new System.Drawing.Point(12, 12);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(1488, 612);
			this.panelMain.TabIndex = 4;
			// 
			// splitterRawParsed
			// 
			this.splitterRawParsed.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitterRawParsed.Location = new System.Drawing.Point(0, 183);
			this.splitterRawParsed.Name = "splitterRawParsed";
			this.splitterRawParsed.Size = new System.Drawing.Size(1488, 3);
			this.splitterRawParsed.TabIndex = 10;
			this.splitterRawParsed.TabStop = false;
			// 
			// feedbackListView
			// 
			this.feedbackListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Status,
            this.ItemId,
            this.Title,
            this.SellerName,
            this.FeedbackLeft,
            this.Result,
            this.TransactionId,
            this.OrderLineItemId,
            this.ProfileName});
			this.feedbackListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.feedbackListView.FullRowSelect = true;
			this.feedbackListView.GridLines = true;
			this.feedbackListView.Location = new System.Drawing.Point(0, 183);
			this.feedbackListView.Name = "feedbackListView";
			this.feedbackListView.Size = new System.Drawing.Size(1488, 429);
			this.feedbackListView.TabIndex = 9;
			this.feedbackListView.UseCompatibleStateImageBehavior = false;
			this.feedbackListView.View = System.Windows.Forms.View.Details;
			// 
			// Status
			// 
			this.Status.Text = "Status";
			this.Status.Width = 90;
			// 
			// ItemId
			// 
			this.ItemId.Text = "Item ID";
			this.ItemId.Width = 105;
			// 
			// Title
			// 
			this.Title.Text = "Title";
			this.Title.Width = 505;
			// 
			// SellerName
			// 
			this.SellerName.Text = "Seller Name";
			this.SellerName.Width = 133;
			// 
			// FeedbackLeft
			// 
			this.FeedbackLeft.Text = "Feedback Left";
			this.FeedbackLeft.Width = 500;
			// 
			// Result
			// 
			this.Result.Text = "API Result";
			this.Result.Width = 150;
			// 
			// TransactionId
			// 
			this.TransactionId.DisplayIndex = 7;
			this.TransactionId.Text = "Transaction ID";
			this.TransactionId.Width = 105;
			// 
			// OrderLineItemId
			// 
			this.OrderLineItemId.DisplayIndex = 8;
			this.OrderLineItemId.Text = "Order Line Item ID";
			this.OrderLineItemId.Width = 125;
			// 
			// ProfileName
			// 
			this.ProfileName.DisplayIndex = 6;
			this.ProfileName.Text = "Profile";
			// 
			// textBoxRawData
			// 
			this.textBoxRawData.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBoxRawData.Location = new System.Drawing.Point(0, 0);
			this.textBoxRawData.Name = "textBoxRawData";
			this.textBoxRawData.Size = new System.Drawing.Size(1488, 183);
			this.textBoxRawData.TabIndex = 7;
			this.textBoxRawData.Text = "Place raw list of item IDs here...";
			this.textBoxRawData.TextChanged += new System.EventHandler(this.textBoxRawData_TextChanged);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 684);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(1512, 25);
			this.statusStrip.TabIndex = 12;
			this.statusStrip.Text = "statusStrip";
			// 
			// toolStripProgressBar
			// 
			this.toolStripProgressBar.Name = "toolStripProgressBar";
			this.toolStripProgressBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.toolStripProgressBar.Size = new System.Drawing.Size(200, 19);
			// 
			// toolStripStatusLabel
			// 
			this.toolStripStatusLabel.Name = "toolStripStatusLabel";
			this.toolStripStatusLabel.Size = new System.Drawing.Size(18, 20);
			this.toolStripStatusLabel.Text = "...";
			// 
			// buttonLeaveFeedback
			// 
			this.buttonLeaveFeedback.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonLeaveFeedback.Location = new System.Drawing.Point(1360, 630);
			this.buttonLeaveFeedback.Name = "buttonLeaveFeedback";
			this.buttonLeaveFeedback.Size = new System.Drawing.Size(140, 45);
			this.buttonLeaveFeedback.TabIndex = 13;
			this.buttonLeaveFeedback.Text = "Leave Feedback";
			this.buttonLeaveFeedback.UseVisualStyleBackColor = true;
			this.buttonLeaveFeedback.Click += new System.EventHandler(this.buttonLeaveFeedback_Click);
			// 
			// buttonStop
			// 
			this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonStop.Enabled = false;
			this.buttonStop.Location = new System.Drawing.Point(1214, 630);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(140, 45);
			this.buttonStop.TabIndex = 14;
			this.buttonStop.Text = "Stop!";
			this.buttonStop.UseVisualStyleBackColor = true;
			this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
			// 
			// toolStripItemCount
			// 
			this.toolStripItemCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.toolStripItemCount.AutoSize = true;
			this.toolStripItemCount.Location = new System.Drawing.Point(12, 630);
			this.toolStripItemCount.Name = "toolStripItemCount";
			this.toolStripItemCount.Size = new System.Drawing.Size(57, 17);
			this.toolStripItemCount.TabIndex = 15;
			this.toolStripItemCount.Text = "Items: 0";
			// 
			// buttonSanitizeList
			// 
			this.buttonSanitizeList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSanitizeList.Location = new System.Drawing.Point(922, 630);
			this.buttonSanitizeList.Name = "buttonSanitizeList";
			this.buttonSanitizeList.Size = new System.Drawing.Size(140, 45);
			this.buttonSanitizeList.TabIndex = 16;
			this.buttonSanitizeList.Text = "Sanitize List";
			this.buttonSanitizeList.UseVisualStyleBackColor = true;
			this.buttonSanitizeList.Click += new System.EventHandler(this.buttonSanitizeList_Click);
			// 
			// buttonClearCompleted
			// 
			this.buttonClearCompleted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClearCompleted.Location = new System.Drawing.Point(1068, 630);
			this.buttonClearCompleted.Name = "buttonClearCompleted";
			this.buttonClearCompleted.Size = new System.Drawing.Size(140, 45);
			this.buttonClearCompleted.TabIndex = 17;
			this.buttonClearCompleted.Text = "Clear Completed";
			this.buttonClearCompleted.UseVisualStyleBackColor = true;
			this.buttonClearCompleted.Click += new System.EventHandler(this.buttonClearCompleted_Click);
			// 
			// buttonItemPicker
			// 
			this.buttonItemPicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonItemPicker.Location = new System.Drawing.Point(122, 630);
			this.buttonItemPicker.Name = "buttonItemPicker";
			this.buttonItemPicker.Size = new System.Drawing.Size(140, 45);
			this.buttonItemPicker.TabIndex = 18;
			this.buttonItemPicker.Text = "Item Picker...";
			this.buttonItemPicker.UseVisualStyleBackColor = true;
			this.buttonItemPicker.Click += new System.EventHandler(this.buttonItemPicker_Click);
			// 
			// buttonIgnoreSelected
			// 
			this.buttonIgnoreSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonIgnoreSelected.Location = new System.Drawing.Point(742, 630);
			this.buttonIgnoreSelected.Name = "buttonIgnoreSelected";
			this.buttonIgnoreSelected.Size = new System.Drawing.Size(140, 45);
			this.buttonIgnoreSelected.TabIndex = 19;
			this.buttonIgnoreSelected.Text = "Ignore Listed Items";
			this.buttonIgnoreSelected.UseVisualStyleBackColor = true;
			this.buttonIgnoreSelected.Click += new System.EventHandler(this.buttonIgnoreListed_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1512, 709);
			this.Controls.Add(this.buttonIgnoreSelected);
			this.Controls.Add(this.buttonItemPicker);
			this.Controls.Add(this.buttonClearCompleted);
			this.Controls.Add(this.buttonSanitizeList);
			this.Controls.Add(this.toolStripItemCount);
			this.Controls.Add(this.buttonStop);
			this.Controls.Add(this.buttonLeaveFeedback);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.panelMain);
			this.Name = "MainForm";
			this.Text = "ebay Leave Bulk Feedback";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.panelMain.ResumeLayout(false);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panelMain;
		private System.Windows.Forms.Splitter splitterRawParsed;
		private System.Windows.Forms.RichTextBox textBoxRawData;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
		private System.Windows.Forms.ColumnHeader Status;
		private System.Windows.Forms.ColumnHeader ItemId;
		private System.Windows.Forms.ColumnHeader Title;
		private System.Windows.Forms.ColumnHeader SellerName;
		private System.Windows.Forms.ColumnHeader FeedbackLeft;
		private System.Windows.Forms.Button buttonLeaveFeedback;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.ColumnHeader Result;
		private System.Windows.Forms.Label toolStripItemCount;
		private System.Windows.Forms.Button buttonSanitizeList;
		private System.Windows.Forms.Button buttonClearCompleted;
		private System.Windows.Forms.Button buttonItemPicker;
		private ListViewNonFlicker feedbackListView;
		private System.Windows.Forms.ColumnHeader TransactionId;
		private System.Windows.Forms.ColumnHeader ProfileName;
		private System.Windows.Forms.ColumnHeader OrderLineItemId;
		private System.Windows.Forms.Button buttonIgnoreSelected;
	}
}