namespace ebayLeaveFeedbackForSellers
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
			this.listViewItems = new System.Windows.Forms.ListView();
			this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ItemId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.SellerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.FeedbackLeft = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Result = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.textBoxRawData = new System.Windows.Forms.RichTextBox();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.buttonLeaveFeedback = new System.Windows.Forms.Button();
			this.buttonStop = new System.Windows.Forms.Button();
			this.labelItemCount = new System.Windows.Forms.Label();
			this.buttonSanitizeList = new System.Windows.Forms.Button();
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
			this.panelMain.Controls.Add(this.listViewItems);
			this.panelMain.Controls.Add(this.textBoxRawData);
			this.panelMain.Location = new System.Drawing.Point(12, 12);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(1158, 597);
			this.panelMain.TabIndex = 4;
			// 
			// splitterRawParsed
			// 
			this.splitterRawParsed.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitterRawParsed.Location = new System.Drawing.Point(0, 183);
			this.splitterRawParsed.Name = "splitterRawParsed";
			this.splitterRawParsed.Size = new System.Drawing.Size(1158, 3);
			this.splitterRawParsed.TabIndex = 10;
			this.splitterRawParsed.TabStop = false;
			// 
			// listViewItems
			// 
			this.listViewItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Status,
            this.ItemId,
            this.Title,
            this.SellerName,
            this.FeedbackLeft,
            this.Result});
			this.listViewItems.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewItems.FullRowSelect = true;
			this.listViewItems.GridLines = true;
			this.listViewItems.Location = new System.Drawing.Point(0, 183);
			this.listViewItems.Name = "listViewItems";
			this.listViewItems.Size = new System.Drawing.Size(1158, 414);
			this.listViewItems.TabIndex = 9;
			this.listViewItems.UseCompatibleStateImageBehavior = false;
			this.listViewItems.View = System.Windows.Forms.View.Details;
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
			// textBoxRawData
			// 
			this.textBoxRawData.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBoxRawData.Location = new System.Drawing.Point(0, 0);
			this.textBoxRawData.Name = "textBoxRawData";
			this.textBoxRawData.Size = new System.Drawing.Size(1158, 183);
			this.textBoxRawData.TabIndex = 7;
			this.textBoxRawData.Text = "Place raw list of item IDs here...";
			this.textBoxRawData.TextChanged += new System.EventHandler(this.textBoxRawData_TextChanged);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 680);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(1182, 25);
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
			this.buttonLeaveFeedback.Location = new System.Drawing.Point(1030, 626);
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
			this.buttonStop.Location = new System.Drawing.Point(884, 626);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(140, 45);
			this.buttonStop.TabIndex = 14;
			this.buttonStop.Text = "Stop!";
			this.buttonStop.UseVisualStyleBackColor = true;
			this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
			// 
			// labelItemCount
			// 
			this.labelItemCount.AutoSize = true;
			this.labelItemCount.Location = new System.Drawing.Point(13, 626);
			this.labelItemCount.Name = "labelItemCount";
			this.labelItemCount.Size = new System.Drawing.Size(89, 17);
			this.labelItemCount.TabIndex = 15;
			this.labelItemCount.Text = "Item count: 0";
			// 
			// buttonSanitizeList
			// 
			this.buttonSanitizeList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSanitizeList.Location = new System.Drawing.Point(738, 626);
			this.buttonSanitizeList.Name = "buttonSanitizeList";
			this.buttonSanitizeList.Size = new System.Drawing.Size(140, 45);
			this.buttonSanitizeList.TabIndex = 16;
			this.buttonSanitizeList.Text = "Sanitize List";
			this.buttonSanitizeList.UseVisualStyleBackColor = true;
			this.buttonSanitizeList.Click += new System.EventHandler(this.buttonSanitizeList_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1182, 705);
			this.Controls.Add(this.buttonSanitizeList);
			this.Controls.Add(this.labelItemCount);
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
		private System.Windows.Forms.ListView listViewItems;
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
		private System.Windows.Forms.Label labelItemCount;
		private System.Windows.Forms.Button buttonSanitizeList;
	}
}