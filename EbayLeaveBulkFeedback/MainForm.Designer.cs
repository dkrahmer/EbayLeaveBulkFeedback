﻿using EbayLeaveBulkFeedback;
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
			this.components = new System.ComponentModel.Container();
			this.contextMenuStripFeedbackList = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewInBrowserToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.contextMenuStripPickListView = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ignoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.buttonRefresh = new System.Windows.Forms.Button();
			this.textBoxSearch = new PlaceholderTextBox.PlaceholderTextBox();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.buttonLeaveFeedback = new System.Windows.Forms.Button();
			this.buttonStop = new System.Windows.Forms.Button();
			this.toolStripItemCount = new System.Windows.Forms.Label();
			this.buttonClearCompleted = new System.Windows.Forms.Button();
			this.buttonRawEntry = new System.Windows.Forms.Button();
			this.buttonIgnoreSelected = new System.Windows.Forms.Button();
			this.buttonConfig = new System.Windows.Forms.Button();
			this.pickListView = new EbayLeaveBulkFeedback.ListViewNonFlicker();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.EndDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Seller = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.TrackingNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.UserProfile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
			this.contextMenuStripFeedbackList.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.contextMenuStripPickListView.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStripFeedbackList
			// 
			this.contextMenuStripFeedbackList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem,
            this.viewInBrowserToolStripMenuItem1});
			this.contextMenuStripFeedbackList.Name = "contextMenuStripFeedbackList";
			this.contextMenuStripFeedbackList.Size = new System.Drawing.Size(158, 48);
			// 
			// removeToolStripMenuItem
			// 
			this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
			this.removeToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.removeToolStripMenuItem.Text = "&Remove";
			this.removeToolStripMenuItem.Click += new System.EventHandler(this.RemoveToolStripMenuItem_Click);
			// 
			// viewInBrowserToolStripMenuItem1
			// 
			this.viewInBrowserToolStripMenuItem1.Name = "viewInBrowserToolStripMenuItem1";
			this.viewInBrowserToolStripMenuItem1.Size = new System.Drawing.Size(157, 22);
			this.viewInBrowserToolStripMenuItem1.Text = "&View in Browser";
			this.viewInBrowserToolStripMenuItem1.Click += new System.EventHandler(this.ViewInBrowserToolStripMenuItem1_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.Location = new System.Drawing.Point(8, 12);
			this.splitContainer1.MinimumSize = new System.Drawing.Size(0, 100);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.pickListView);
			this.splitContainer1.Panel1.Controls.Add(this.buttonRefresh);
			this.splitContainer1.Panel1.Controls.Add(this.textBoxSearch);
			this.splitContainer1.Panel1MinSize = 200;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.feedbackListView);
			this.splitContainer1.Panel2MinSize = 75;
			this.splitContainer1.Size = new System.Drawing.Size(1407, 727);
			this.splitContainer1.SplitterDistance = 578;
			this.splitContainer1.TabIndex = 9;
			// 
			// contextMenuStripPickListView
			// 
			this.contextMenuStripPickListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.ignoreToolStripMenuItem,
            this.viewInBrowserToolStripMenuItem});
			this.contextMenuStripPickListView.Name = "contextMenuStripPickListView";
			this.contextMenuStripPickListView.Size = new System.Drawing.Size(158, 70);
			// 
			// addToolStripMenuItem
			// 
			this.addToolStripMenuItem.Name = "addToolStripMenuItem";
			this.addToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.addToolStripMenuItem.Text = "&Add";
			this.addToolStripMenuItem.Click += new System.EventHandler(this.AddToolStripMenuItem_Click);
			// 
			// ignoreToolStripMenuItem
			// 
			this.ignoreToolStripMenuItem.Enabled = false;
			this.ignoreToolStripMenuItem.Name = "ignoreToolStripMenuItem";
			this.ignoreToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.ignoreToolStripMenuItem.Text = "&Ignore";
			this.ignoreToolStripMenuItem.Click += new System.EventHandler(this.IgnoreToolStripMenuItem_Click);
			// 
			// viewInBrowserToolStripMenuItem
			// 
			this.viewInBrowserToolStripMenuItem.Name = "viewInBrowserToolStripMenuItem";
			this.viewInBrowserToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.viewInBrowserToolStripMenuItem.Text = "&View in Browser";
			this.viewInBrowserToolStripMenuItem.Click += new System.EventHandler(this.ViewInBrowserToolStripMenuItem_Click);
			// 
			// buttonRefresh
			// 
			this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRefresh.Location = new System.Drawing.Point(1332, -1);
			this.buttonRefresh.Margin = new System.Windows.Forms.Padding(2);
			this.buttonRefresh.Name = "buttonRefresh";
			this.buttonRefresh.Size = new System.Drawing.Size(75, 21);
			this.buttonRefresh.TabIndex = 8;
			this.buttonRefresh.Text = "Refresh";
			this.buttonRefresh.UseVisualStyleBackColor = true;
			this.buttonRefresh.Click += new System.EventHandler(this.ButtonRefresh_Click);
			// 
			// textBoxSearch
			// 
			this.textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxSearch.Location = new System.Drawing.Point(0, 0);
			this.textBoxSearch.Margin = new System.Windows.Forms.Padding(2);
			this.textBoxSearch.Name = "textBoxSearch";
			this.textBoxSearch.PlaceholderText = "Search";
			this.textBoxSearch.Size = new System.Drawing.Size(262, 20);
			this.textBoxSearch.TabIndex = 6;
			this.textBoxSearch.TextChanged += new System.EventHandler(this.TextBoxSearch_TextChanged);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 786);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
			this.statusStrip.Size = new System.Drawing.Size(1424, 22);
			this.statusStrip.TabIndex = 12;
			this.statusStrip.Text = "statusStrip";
			// 
			// toolStripProgressBar
			// 
			this.toolStripProgressBar.Name = "toolStripProgressBar";
			this.toolStripProgressBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.toolStripProgressBar.Size = new System.Drawing.Size(150, 16);
			// 
			// toolStripStatusLabel
			// 
			this.toolStripStatusLabel.Name = "toolStripStatusLabel";
			this.toolStripStatusLabel.Size = new System.Drawing.Size(16, 17);
			this.toolStripStatusLabel.Text = "...";
			// 
			// buttonLeaveFeedback
			// 
			this.buttonLeaveFeedback.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonLeaveFeedback.Location = new System.Drawing.Point(1310, 744);
			this.buttonLeaveFeedback.Margin = new System.Windows.Forms.Padding(2);
			this.buttonLeaveFeedback.Name = "buttonLeaveFeedback";
			this.buttonLeaveFeedback.Size = new System.Drawing.Size(105, 37);
			this.buttonLeaveFeedback.TabIndex = 13;
			this.buttonLeaveFeedback.Text = "Leave Feedback";
			this.buttonLeaveFeedback.UseVisualStyleBackColor = true;
			this.buttonLeaveFeedback.Click += new System.EventHandler(this.ButtonLeaveFeedback_Click);
			// 
			// buttonStop
			// 
			this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonStop.Enabled = false;
			this.buttonStop.Location = new System.Drawing.Point(1200, 744);
			this.buttonStop.Margin = new System.Windows.Forms.Padding(2);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(105, 37);
			this.buttonStop.TabIndex = 14;
			this.buttonStop.Text = "Stop!";
			this.buttonStop.UseVisualStyleBackColor = true;
			this.buttonStop.Click += new System.EventHandler(this.ButtonStop_Click);
			// 
			// toolStripItemCount
			// 
			this.toolStripItemCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.toolStripItemCount.AutoSize = true;
			this.toolStripItemCount.Location = new System.Drawing.Point(9, 744);
			this.toolStripItemCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.toolStripItemCount.Name = "toolStripItemCount";
			this.toolStripItemCount.Size = new System.Drawing.Size(44, 13);
			this.toolStripItemCount.TabIndex = 15;
			this.toolStripItemCount.Text = "Items: 0";
			// 
			// buttonClearCompleted
			// 
			this.buttonClearCompleted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClearCompleted.Location = new System.Drawing.Point(1091, 744);
			this.buttonClearCompleted.Margin = new System.Windows.Forms.Padding(2);
			this.buttonClearCompleted.Name = "buttonClearCompleted";
			this.buttonClearCompleted.Size = new System.Drawing.Size(105, 37);
			this.buttonClearCompleted.TabIndex = 17;
			this.buttonClearCompleted.Text = "Clear Completed";
			this.buttonClearCompleted.UseVisualStyleBackColor = true;
			this.buttonClearCompleted.Click += new System.EventHandler(this.ButtonClearCompleted_Click);
			// 
			// btnRawEntry
			// 
			this.buttonRawEntry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonRawEntry.Location = new System.Drawing.Point(92, 744);
			this.buttonRawEntry.Margin = new System.Windows.Forms.Padding(2);
			this.buttonRawEntry.Name = "btnRawEntry";
			this.buttonRawEntry.Size = new System.Drawing.Size(105, 37);
			this.buttonRawEntry.TabIndex = 18;
			this.buttonRawEntry.Text = "Raw Entry...";
			this.buttonRawEntry.UseVisualStyleBackColor = true;
			this.buttonRawEntry.Click += new System.EventHandler(this.BtnRawEntry_Click);
			// 
			// buttonIgnoreSelected
			// 
			this.buttonIgnoreSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonIgnoreSelected.Location = new System.Drawing.Point(982, 744);
			this.buttonIgnoreSelected.Margin = new System.Windows.Forms.Padding(2);
			this.buttonIgnoreSelected.Name = "buttonIgnoreSelected";
			this.buttonIgnoreSelected.Size = new System.Drawing.Size(105, 37);
			this.buttonIgnoreSelected.TabIndex = 19;
			this.buttonIgnoreSelected.Text = "Ignore Listed Items";
			this.buttonIgnoreSelected.UseVisualStyleBackColor = true;
			this.buttonIgnoreSelected.Click += new System.EventHandler(this.ButtonIgnoreListed_Click);
			// 
			// buttonConfig
			// 
			this.buttonConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonConfig.Location = new System.Drawing.Point(201, 744);
			this.buttonConfig.Margin = new System.Windows.Forms.Padding(2);
			this.buttonConfig.Name = "buttonConfig";
			this.buttonConfig.Size = new System.Drawing.Size(105, 37);
			this.buttonConfig.TabIndex = 20;
			this.buttonConfig.Text = "Config...";
			this.buttonConfig.UseVisualStyleBackColor = true;
			this.buttonConfig.Click += new System.EventHandler(this.ButtonConfig_Click);
			// 
			// pickListView
			// 
			this.pickListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pickListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.EndDate,
            this.columnHeader2,
            this.Seller,
            this.Price,
            this.TrackingNumber,
            this.UserProfile});
			this.pickListView.ContextMenuStrip = this.contextMenuStripPickListView;
			this.pickListView.Location = new System.Drawing.Point(0, 24);
			this.pickListView.Margin = new System.Windows.Forms.Padding(2);
			this.pickListView.Name = "pickListView";
			this.pickListView.Size = new System.Drawing.Size(1407, 554);
			this.pickListView.TabIndex = 5;
			this.pickListView.TileSize = new System.Drawing.Size(315, 150);
			this.pickListView.UseCompatibleStateImageBehavior = false;
			this.pickListView.View = System.Windows.Forms.View.Tile;
			this.pickListView.DoubleClick += new System.EventHandler(this.ListViewItems_DoubleClick);
			this.pickListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PickListView_KeyDown);
			this.pickListView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PickListView_KeyPress);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Title";
			// 
			// EndDate
			// 
			this.EndDate.Text = "End Date";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Item ID";
			// 
			// Seller
			// 
			this.Seller.DisplayIndex = 4;
			this.Seller.Text = "Seller";
			// 
			// Price
			// 
			this.Price.DisplayIndex = 5;
			this.Price.Text = "Price";
			// 
			// TrackingNumber
			// 
			this.TrackingNumber.DisplayIndex = 6;
			this.TrackingNumber.Text = "TrackingNumber";
			// 
			// UserProfile
			// 
			this.UserProfile.DisplayIndex = 3;
			this.UserProfile.Text = "User Profile";
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
			this.feedbackListView.ContextMenuStrip = this.contextMenuStripFeedbackList;
			this.feedbackListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.feedbackListView.FullRowSelect = true;
			this.feedbackListView.GridLines = true;
			this.feedbackListView.Location = new System.Drawing.Point(0, 0);
			this.feedbackListView.Margin = new System.Windows.Forms.Padding(2);
			this.feedbackListView.Name = "feedbackListView";
			this.feedbackListView.Size = new System.Drawing.Size(1407, 145);
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
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1424, 808);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.buttonConfig);
			this.Controls.Add(this.buttonIgnoreSelected);
			this.Controls.Add(this.buttonRawEntry);
			this.Controls.Add(this.buttonClearCompleted);
			this.Controls.Add(this.toolStripItemCount);
			this.Controls.Add(this.buttonStop);
			this.Controls.Add(this.buttonLeaveFeedback);
			this.Controls.Add(this.statusStrip);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MinimumSize = new System.Drawing.Size(800, 400);
			this.Name = "MainForm";
			this.Text = "ebay Leave Bulk Feedback";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.contextMenuStripFeedbackList.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.contextMenuStripPickListView.ResumeLayout(false);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
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
		private System.Windows.Forms.Button buttonClearCompleted;
		private System.Windows.Forms.Button buttonRawEntry;
		private ListViewNonFlicker feedbackListView;
		private System.Windows.Forms.ColumnHeader TransactionId;
		private System.Windows.Forms.ColumnHeader ProfileName;
		private System.Windows.Forms.ColumnHeader OrderLineItemId;
		private System.Windows.Forms.Button buttonIgnoreSelected;
		private System.Windows.Forms.Button buttonConfig;
		private System.Windows.Forms.Button buttonRefresh;
		private ListViewNonFlicker pickListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader EndDate;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader Seller;
		private System.Windows.Forms.ColumnHeader Price;
		private System.Windows.Forms.ColumnHeader TrackingNumber;
		private System.Windows.Forms.ColumnHeader UserProfile;
		private PlaceholderTextBox.PlaceholderTextBox textBoxSearch;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripFeedbackList;
		private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripPickListView;
		private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewInBrowserToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ignoreToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewInBrowserToolStripMenuItem1;
		private System.Windows.Forms.SplitContainer splitContainer1;
	}
}