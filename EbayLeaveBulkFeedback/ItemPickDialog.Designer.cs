namespace EbayLeaveBulkFeedback
{
	partial class ItemPickDialog
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
			this.pickListView = new EbayLeaveBulkFeedback.ListViewNonFlicker();
			this.Title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.EndDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ItemId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Seller = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.UserProfile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripItemCount = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.textBoxSearch = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.Price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pickListView
			// 
			this.pickListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pickListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Title,
            this.EndDate,
            this.ItemId,
            this.Seller,
            this.Price,
            this.UserProfile});
			this.pickListView.Location = new System.Drawing.Point(0, 38);
			this.pickListView.Name = "pickListView";
			this.pickListView.Size = new System.Drawing.Size(1017, 581);
			this.pickListView.TabIndex = 0;
			this.pickListView.TileSize = new System.Drawing.Size(315, 150);
			this.pickListView.UseCompatibleStateImageBehavior = false;
			this.pickListView.View = System.Windows.Forms.View.Tile;
			this.pickListView.DoubleClick += new System.EventHandler(this.listViewItems_DoubleClick);
			this.pickListView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pickListView_MouseUp);
			// 
			// Title
			// 
			this.Title.Text = "Title";
			// 
			// EndDate
			// 
			this.EndDate.Text = "End Date";
			// 
			// ItemId
			// 
			this.ItemId.Text = "Item ID";
			// 
			// Seller
			// 
			this.Seller.DisplayIndex = 4;
			this.Seller.Text = "Seller";
			// 
			// UserProfile
			// 
			this.UserProfile.DisplayIndex = 3;
			this.UserProfile.Text = "User Profile";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripItemCount,
            this.toolStripStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 622);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1017, 25);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripProgressBar
			// 
			this.toolStripProgressBar.Name = "toolStripProgressBar";
			this.toolStripProgressBar.Size = new System.Drawing.Size(100, 19);
			// 
			// toolStripItemCount
			// 
			this.toolStripItemCount.Name = "toolStripItemCount";
			this.toolStripItemCount.Size = new System.Drawing.Size(60, 20);
			this.toolStripItemCount.Text = "Items: 0";
			this.toolStripItemCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripStatusLabel
			// 
			this.toolStripStatusLabel.Name = "toolStripStatusLabel";
			this.toolStripStatusLabel.Size = new System.Drawing.Size(18, 20);
			this.toolStripStatusLabel.Text = "...";
			this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxSearch
			// 
			this.textBoxSearch.Location = new System.Drawing.Point(12, 10);
			this.textBoxSearch.Name = "textBoxSearch";
			this.textBoxSearch.Size = new System.Drawing.Size(348, 22);
			this.textBoxSearch.TabIndex = 2;
			this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(366, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(445, 17);
			this.label1.TabIndex = 3;
			this.label1.Text = "Double-click or right-click highlighted items to pick items for feedback.";
			// 
			// Price
			// 
			this.Price.DisplayIndex = 5;
			this.Price.Text = "Price";
			// 
			// ItemPickDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1017, 647);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxSearch);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.pickListView);
			this.Name = "ItemPickDialog";
			this.Text = "Items Ready For Feedback";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ItemListDialog_FormClosing);
			this.Load += new System.EventHandler(this.ItemPickDialog_Load);
			this.Shown += new System.EventHandler(this.ItemPickDialog_Shown);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ColumnHeader Title;
		private System.Windows.Forms.ColumnHeader EndDate;
		private System.Windows.Forms.ColumnHeader ItemId;
		private ListViewNonFlicker pickListView;
		private System.Windows.Forms.ColumnHeader UserProfile;
		private System.Windows.Forms.ColumnHeader Seller;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel toolStripItemCount;
		private System.Windows.Forms.TextBox textBoxSearch;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ColumnHeader Price;
	}
}