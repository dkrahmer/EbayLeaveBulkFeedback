namespace EbayLeaveBulkFeedback
{
	partial class RawEntryDialog
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
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripItemCount = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.textBoxRawData = new System.Windows.Forms.RichTextBox();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.rawEntryListView = new EbayLeaveBulkFeedback.ListViewNonFlicker();
			this.ItemId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripItemCount,
            this.toolStripStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 472);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
			this.statusStrip1.Size = new System.Drawing.Size(738, 22);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripItemCount
			// 
			this.toolStripItemCount.Name = "toolStripItemCount";
			this.toolStripItemCount.Size = new System.Drawing.Size(48, 17);
			this.toolStripItemCount.Text = "Items: 0";
			this.toolStripItemCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripStatusLabel
			// 
			this.toolStripStatusLabel.Name = "toolStripStatusLabel";
			this.toolStripStatusLabel.Size = new System.Drawing.Size(16, 17);
			this.toolStripStatusLabel.Text = "...";
			this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxRawData
			// 
			this.textBoxRawData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxRawData.Location = new System.Drawing.Point(15, 24);
			this.textBoxRawData.Margin = new System.Windows.Forms.Padding(2);
			this.textBoxRawData.Name = "textBoxRawData";
			this.textBoxRawData.Size = new System.Drawing.Size(576, 417);
			this.textBoxRawData.TabIndex = 8;
			this.textBoxRawData.Text = "Place raw list of item IDs here...";
			this.textBoxRawData.TextChanged += new System.EventHandler(this.textBoxRawData_TextChanged);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.Location = new System.Drawing.Point(570, 446);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(75, 23);
			this.btnAdd.TabIndex = 9;
			this.btnAdd.Text = "Add";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(651, 446);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 10;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(122, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "Raw Entry (copy/paste):";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(592, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(82, 13);
			this.label2.TabIndex = 12;
			this.label2.Text = "Found Item IDs:";
			// 
			// rawEntryListView
			// 
			this.rawEntryListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rawEntryListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ItemId});
			this.rawEntryListView.FullRowSelect = true;
			this.rawEntryListView.GridLines = true;
			this.rawEntryListView.Location = new System.Drawing.Point(595, 24);
			this.rawEntryListView.Margin = new System.Windows.Forms.Padding(2);
			this.rawEntryListView.Name = "rawEntryListView";
			this.rawEntryListView.Size = new System.Drawing.Size(131, 417);
			this.rawEntryListView.TabIndex = 13;
			this.rawEntryListView.UseCompatibleStateImageBehavior = false;
			this.rawEntryListView.View = System.Windows.Forms.View.Details;
			// 
			// ItemId
			// 
			this.ItemId.Text = "Item ID";
			this.ItemId.Width = 105;
			// 
			// RawEntryDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(738, 494);
			this.Controls.Add(this.rawEntryListView);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.textBoxRawData);
			this.Controls.Add(this.statusStrip1);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "RawEntryDialog";
			this.Text = "Raw Entry";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ItemListDialog_FormClosing);
			this.Load += new System.EventHandler(this.RawEntryDialog_Load);
			this.Shown += new System.EventHandler(this.RawEntryDialog_Shown);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel toolStripItemCount;
		private System.Windows.Forms.RichTextBox textBoxRawData;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private ListViewNonFlicker rawEntryListView;
		private System.Windows.Forms.ColumnHeader ItemId;
	}
}