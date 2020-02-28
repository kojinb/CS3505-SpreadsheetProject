namespace SpreadsheetGUI
{
    partial class SpreadsheetForm
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
            this.spreadsheetPanel = new SS.SpreadsheetPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeCellSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCellContentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.additionalFeaturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeCellSelectionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.changeSpreadsheetBorderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borderColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yellowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedCellContentsTextBox = new System.Windows.Forms.TextBox();
            this.selectedCellNameTextBox = new System.Windows.Forms.TextBox();
            this.selectedCellValueTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // spreadsheetPanel
            // 
            this.spreadsheetPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadsheetPanel.Location = new System.Drawing.Point(0, 48);
            this.spreadsheetPanel.Margin = new System.Windows.Forms.Padding(2);
            this.spreadsheetPanel.Name = "spreadsheetPanel";
            this.spreadsheetPanel.Size = new System.Drawing.Size(483, 225);
            this.spreadsheetPanel.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.borderColorToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(484, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 22);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeCellSelectionToolStripMenuItem,
            this.editCellContentsToolStripMenuItem,
            this.additionalFeaturesToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 22);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // changeCellSelectionToolStripMenuItem
            // 
            this.changeCellSelectionToolStripMenuItem.Name = "changeCellSelectionToolStripMenuItem";
            this.changeCellSelectionToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.changeCellSelectionToolStripMenuItem.Text = "ChangeCellSelection";
            this.changeCellSelectionToolStripMenuItem.Click += new System.EventHandler(this.changeCellSelectionToolStripMenuItem_Click);
            // 
            // editCellContentsToolStripMenuItem
            // 
            this.editCellContentsToolStripMenuItem.Name = "editCellContentsToolStripMenuItem";
            this.editCellContentsToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.editCellContentsToolStripMenuItem.Text = "EditCellContents";
            this.editCellContentsToolStripMenuItem.Click += new System.EventHandler(this.editCellContentsToolStripMenuItem_Click);
            // 
            // additionalFeaturesToolStripMenuItem
            // 
            this.additionalFeaturesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeCellSelectionToolStripMenuItem1,
            this.changeSpreadsheetBorderToolStripMenuItem});
            this.additionalFeaturesToolStripMenuItem.Name = "additionalFeaturesToolStripMenuItem";
            this.additionalFeaturesToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.additionalFeaturesToolStripMenuItem.Text = "AdditionalFeatures";
            // 
            // changeCellSelectionToolStripMenuItem1
            // 
            this.changeCellSelectionToolStripMenuItem1.Name = "changeCellSelectionToolStripMenuItem1";
            this.changeCellSelectionToolStripMenuItem1.Size = new System.Drawing.Size(220, 22);
            this.changeCellSelectionToolStripMenuItem1.Text = "Change Cell Selection";
            this.changeCellSelectionToolStripMenuItem1.Click += new System.EventHandler(this.ChangeCellSelectionToolStripMenuItem1_Click);
            // 
            // changeSpreadsheetBorderToolStripMenuItem
            // 
            this.changeSpreadsheetBorderToolStripMenuItem.Name = "changeSpreadsheetBorderToolStripMenuItem";
            this.changeSpreadsheetBorderToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.changeSpreadsheetBorderToolStripMenuItem.Text = "Change Spreadsheet Border";
            this.changeSpreadsheetBorderToolStripMenuItem.Click += new System.EventHandler(this.ChangeSpreadsheetBorderToolStripMenuItem_Click);
            // 
            // borderColorToolStripMenuItem
            // 
            this.borderColorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultToolStripMenuItem,
            this.blueToolStripMenuItem,
            this.redToolStripMenuItem,
            this.yellowToolStripMenuItem,
            this.greenToolStripMenuItem,
            this.greyToolStripMenuItem});
            this.borderColorToolStripMenuItem.Name = "borderColorToolStripMenuItem";
            this.borderColorToolStripMenuItem.Size = new System.Drawing.Size(86, 22);
            this.borderColorToolStripMenuItem.Text = "Border Color";
            // 
            // defaultToolStripMenuItem
            // 
            this.defaultToolStripMenuItem.Name = "defaultToolStripMenuItem";
            this.defaultToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.defaultToolStripMenuItem.Text = "Default";
            this.defaultToolStripMenuItem.Click += new System.EventHandler(this.DefaultToolStripMenuItem_Click);
            // 
            // blueToolStripMenuItem
            // 
            this.blueToolStripMenuItem.Name = "blueToolStripMenuItem";
            this.blueToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.blueToolStripMenuItem.Text = "Blue";
            this.blueToolStripMenuItem.Click += new System.EventHandler(this.BlueToolStripMenuItem_Click);
            // 
            // redToolStripMenuItem
            // 
            this.redToolStripMenuItem.Name = "redToolStripMenuItem";
            this.redToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.redToolStripMenuItem.Text = "Red";
            this.redToolStripMenuItem.Click += new System.EventHandler(this.RedToolStripMenuItem_Click);
            // 
            // yellowToolStripMenuItem
            // 
            this.yellowToolStripMenuItem.Name = "yellowToolStripMenuItem";
            this.yellowToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.yellowToolStripMenuItem.Text = "Yellow";
            this.yellowToolStripMenuItem.Click += new System.EventHandler(this.YellowToolStripMenuItem_Click);
            // 
            // greenToolStripMenuItem
            // 
            this.greenToolStripMenuItem.Name = "greenToolStripMenuItem";
            this.greenToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.greenToolStripMenuItem.Text = "Green";
            this.greenToolStripMenuItem.Click += new System.EventHandler(this.GreenToolStripMenuItem_Click);
            // 
            // greyToolStripMenuItem
            // 
            this.greyToolStripMenuItem.Name = "greyToolStripMenuItem";
            this.greyToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.greyToolStripMenuItem.Text = "Gray";
            this.greyToolStripMenuItem.Click += new System.EventHandler(this.GreyToolStripMenuItem_Click);
            // 
            // selectedCellContentsTextBox
            // 
            this.selectedCellContentsTextBox.Location = new System.Drawing.Point(142, 23);
            this.selectedCellContentsTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.selectedCellContentsTextBox.Name = "selectedCellContentsTextBox";
            this.selectedCellContentsTextBox.Size = new System.Drawing.Size(52, 20);
            this.selectedCellContentsTextBox.TabIndex = 2;
            this.selectedCellContentsTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.selectedCellContentsTextBox_KeyDown);
            // 
            // selectedCellNameTextBox
            // 
            this.selectedCellNameTextBox.Location = new System.Drawing.Point(6, 23);
            this.selectedCellNameTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.selectedCellNameTextBox.Name = "selectedCellNameTextBox";
            this.selectedCellNameTextBox.Size = new System.Drawing.Size(52, 20);
            this.selectedCellNameTextBox.TabIndex = 4;
            // 
            // selectedCellValueTextBox
            // 
            this.selectedCellValueTextBox.Location = new System.Drawing.Point(74, 23);
            this.selectedCellValueTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.selectedCellValueTextBox.Name = "selectedCellValueTextBox";
            this.selectedCellValueTextBox.Size = new System.Drawing.Size(52, 20);
            this.selectedCellValueTextBox.TabIndex = 5;
            // 
            // SpreadsheetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 271);
            this.Controls.Add(this.selectedCellValueTextBox);
            this.Controls.Add(this.selectedCellNameTextBox);
            this.Controls.Add(this.selectedCellContentsTextBox);
            this.Controls.Add(this.spreadsheetPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SpreadsheetForm";
            this.Text = "SpreadsheetForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpreadsheetForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SS.SpreadsheetPanel spreadsheetPanel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.TextBox selectedCellContentsTextBox;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.TextBox selectedCellNameTextBox;
        private System.Windows.Forms.TextBox selectedCellValueTextBox;
        private System.Windows.Forms.ToolStripMenuItem changeCellSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCellContentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem additionalFeaturesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem borderColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yellowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeCellSelectionToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem changeSpreadsheetBorderToolStripMenuItem;
    }
}

