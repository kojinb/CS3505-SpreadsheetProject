// Author: Hui Wang & Max Chen
// Description: Spreadsheet GUI project for PS6 of CS3500
// Date: Fall, 2019
// Version: 2.0

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SpreadsheetUtilities;
using SS;

namespace SpreadsheetGUI
{
    public partial class Form1 : Form
    {
        // Pop-up dialog for opening and saving files
        OpenFileDialog ofd = new OpenFileDialog();
        SaveFileDialog sfd = new SaveFileDialog();

        // Number of tab pages
        private int numOfPages = 0;

        // Collection of spreadsheets
        private Dictionary<TabPage, SpreadsheetPanel> panelDic = new Dictionary<TabPage, SpreadsheetPanel>();
        private Dictionary<TabPage, Spreadsheet> sheetDic = new Dictionary<TabPage, Spreadsheet>();
        private Dictionary<TabPage, string> fileDic = new Dictionary<TabPage, string>();

        /// <summary>
        /// Check if cell name is valid or not.
        /// One letter followed by one ~ two digits.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool IsValid(string name)
        {
            return Regex.IsMatch(name, @"^[a-zA-Z][1-9][0-9]?$");
        }

        /// <summary>
        /// Constructor to initialize an empty spreadsheet.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            tabControl1.SelectTab("tabPage1");

            TabPage tp = tabControl1.SelectedTab;

            spreadsheetPanel.SelectionChanged += SelectionChangedHandler;

            panelDic.Add(tp, spreadsheetPanel);
            sheetDic.Add(tp, new Spreadsheet(IsValid, s => s.ToUpper(), "ps6"));

            tp.Controls.Add(spreadsheetPanel);

            numOfPages++;

            UpdateSpreadsheet();
        }

        /// <summary>
        /// Return current selected cell's name.
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetCellName(int col, int row)
        {
            return "" + (char)(col + 'A') + (row + 1);
        }

        /// <summary>
        /// Return the column and row of current selected cell.
        /// </summary>
        /// <param name="cellName"></param>
        /// <param name="col"></param>
        /// <param name="row"></param>
        private void GetColandRow(string cellName, out int col, out int row)
        {
            string s = "";
            char[] arr = cellName.ToCharArray();
            col = arr[0] - 'A';
            for (int i = 1; i < arr.Length; i++)
            {
                s += arr[i];
            }
            row = int.Parse(s) - 1;
        }

        /// <summary>
        /// Update the spreadsheet.
        /// </summary>
        private void UpdateSpreadsheet()
        {
            // Auto focus on the text box for immediate typing
            contentTextBox.Focus();

            Spreadsheet spreadsheet = sheetDic[tabControl1.SelectedTab];
            panelDic[tabControl1.SelectedTab].GetSelection(out int col, out int row);
                       
            String cellName = GetCellName(col, row);
            Object cellContents = spreadsheet.GetCellContents(cellName);
            Object cellValue = spreadsheet.GetCellValue(cellName);

            // Display selected cell name         
            cellNameTextBox.Text = cellName;

            // When cell contains formula, display the formula with prefix "="
            if (spreadsheet.GetCellContents(cellName) is Formula)
            {
                contentTextBox.Text = "=" + cellContents.ToString();
            }              
            else
            {
                contentTextBox.Text = cellContents.ToString();
            }
                
            // When cell value is FormulaError, display error message
            if (spreadsheet.GetCellValue(cellName) is FormulaError)
            {
                valueTextBox.Text = ((FormulaError)cellValue).Reason;
            }
            else
            {
                valueTextBox.Text = cellValue.ToString();
            }            
        }

        /// <summary>
        /// Update and re-evaluate cells when new input is entered in content text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentTextBox_TextChanged(object sender, EventArgs e)
        {
            
            ISet<String> dependents = null;
            if(sheetDic.Count == 0)
            {
                return;
            }

            Spreadsheet spreadsheet = sheetDic[tabControl1.SelectedTab];

            try
            {               
                // Set the cell contents and get all other cells that need to be updated
                dependents = spreadsheet.SetContentsOfCell(cellNameTextBox.Text, contentTextBox.Text).ToHashSet();
            }
            catch (CircularException)
            {
                MessageBox.Show("Error: A circular reference error occured. Please double check your formula input.", "Circular Reference Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormulaFormatException)
            {
                MessageBox.Show("Error: Invalid formula format. Please double check your formula input.", "Formula Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //catch (ArgumentNullException)
            //{
            //    MessageBox.Show("You must have a spreadsheet open.");
            //}

            // Update the spreadsheet
            UpdateSpreadsheet();

            // If there are no dependents, just return
            if (ReferenceEquals(dependents, null))
                return;

            // Update each cell
            foreach (string cell in dependents)
                UpdateCell(spreadsheet, cell);
        }

        /// <summary>
        /// Re-evaluate each individual cell.
        /// </summary>
        /// <param name="spreadsheet"></param>
        /// <param name="cellName"></param>
        private void UpdateCell(Spreadsheet spreadsheet, string cellName)
        {
            GetColandRow(cellName, out int col, out int row);
            string cellValue = spreadsheet.GetCellValue(cellName).ToString();

            // Display cell value in spreadsheetPanel
            panelDic[tabControl1.SelectedTab].SetValue(col, row, cellValue);
        }

        /// <summary>
        /// When "Enter" key is pressed, call ContentTextBox_TextChanged to initiate evaluation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentTextBox_KeyDown(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('\r'))
                ContentTextBox_TextChanged(sender, e);
        }

        /// <summary>
        /// Handle selection changed.
        /// </summary>
        /// <param name="p"></param>
        private void SelectionChangedHandler(SpreadsheetPanel p)
        {
            p.GetSelection(out int col, out int row);
            p.GetValue(col, row, out string val);
            string cellName = GetCellName(col, row);
            cellNameTextBox.Text = cellName;
            valueTextBox.Text = val;

            UpdateSpreadsheet();
        }

        /// <summary>
        /// Close current tab and remove spread sheet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveSheetButton_Click(object sender, EventArgs e)
        {
            TabPage currentTab = tabControl1.SelectedTab;

            panelDic.Remove(currentTab);
            sheetDic.Remove(currentTab);
            fileDic.Remove(currentTab);
            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            // When there is no more tabs
            if (panelDic.Count == 0)
            {
                removeSheetButton.Enabled = false;
                cellNameTextBox.Text = "";
                valueTextBox.Text = "";
            }
        }

        /// <summary>
        /// Add a new empty spreadsheet to a new tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddSheetButton_Click(object sender, EventArgs e)
        {
            numOfPages++;
            removeSheetButton.Enabled = true;

            TabPage tp = new TabPage("tabPage" + numOfPages);

            SpreadsheetPanel sp = new SpreadsheetPanel();
            sp.Dock = DockStyle.Fill;

            sp.SelectionChanged += SelectionChangedHandler;

            panelDic.Add(tp, sp);
            sheetDic.Add(tp, new Spreadsheet(IsValid, s => s.ToUpper(), "ps6"));

            tp.Controls.Add(sp);
            tabControl1.TabPages.Add(tp);
            // Whenever a new tab is added, add the spreadsheet to collection
            if (panelDic.Count == 1)
            {
                panelDic[tabControl1.SelectedTab].GetSelection(out int col, out int row);
                cellNameTextBox.Text = GetCellName(col, row);
                //panelDic[tabControl1.SelectedTab].GetValue(0, 0, out string val);
                valueTextBox.Text = "";
            }

            UpdateSpreadsheet();
        }

        /// <summary>
        /// Save the current spreadsheet to file, pop-up dialog for choosing directory.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TabPage currentTab = tabControl1.SelectedTab;

                sfd.RestoreDirectory = true;
                sfd.FileName = "*.sprd";
                sfd.DefaultExt = "sprd";
                sfd.Filter = "SPRD |*.sprd";

                // Pop-up dialog, save the spreadsheet
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Spreadsheet sheet = new Spreadsheet(IsValid, s => s.ToUpper(), "ps6");
                    sheetDic[currentTab].Save(sfd.FileName);
                    sheetDic[currentTab] = sheet;
                    fileDic[currentTab] = sfd.FileName;
                    sheet.IsSaved = true;
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("You must have a spreadsheet open.");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// Save the current spreadsheet to a file. 
        /// Save changes to the same file if it already exists.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TabPage currentTab = tabControl1.SelectedTab;
                // If file doesn't exist, create new file
                if (!fileDic.ContainsKey(currentTab))
                {
                    sfd.RestoreDirectory = true;
                    sfd.FileName = "*.sprd";
                    sfd.DefaultExt = "sprd";
                    sfd.Filter = "SPRD |*.sprd";
                    // Pop-up dialog
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        sheetDic[currentTab].Save(sfd.FileName);
                        fileDic.Add(currentTab, sfd.FileName);
                    }
                }
                // Else, override the file
                else
                {
                    sheetDic[currentTab].Save(fileDic[currentTab]);
                }

            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("You must have a spreadsheet open.");

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }

        /// <summary>
        /// Open a spreadsheet from file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                ofd.DefaultExt = "sprd";
                ofd.Filter = "SPRD (*.sprd)|*.sprd|All files (*.*)|*.*";
                ofd.AddExtension = true;
                // Pop-up dialog
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string fileName = ofd.FileName;
                    Spreadsheet sheet = new Spreadsheet(fileName, IsValid, s => s.ToUpper(), "ps6");

                    TabPage currentTab = tabControl1.SelectedTab;

                    panelDic[currentTab].GetSelection(out int startCol, out int startRow);
                    panelDic[currentTab].GetValue(startCol, startRow, out string val);
                    // Safety feature, when try to open a file and override the current non-empty spreadsheet
                    if (!sheet.IsSaved)
                    {
                        DialogResult dialogResult = MessageBox.Show("Do you want to override your data?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                    // Reset spreadsheet and clear the spreadsheetPanel
                    sheetDic[currentTab] = sheet;
                    fileDic[currentTab] = ofd.FileName;
                    panelDic[currentTab].Clear();


                    // Display all cells' value on the panel
                    foreach (string cellName in sheet.GetNamesOfAllNonemptyCells())
                    {
                        UpdateCell(sheet, cellName);
                    }
                    panelDic[currentTab].GetValue(startCol, startRow, out string newVal);
                    if (val != newVal)
                    {
                        valueTextBox.Text = newVal;
                    }
                    sheet.IsSaved = true;
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("You must have a spreadsheet open.");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// Create a new spreadsheet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
        }

        /// <summary>
        /// Close the spreadsheet window by invoking Form1_FormClosing method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Close the spreadsheet window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Spreadsheet sheet in sheetDic.Values)
            {
                // If there are any changes that not saved, pop-up alert
                if (!sheet.IsSaved)
                {
                    DialogResult dialogResult = MessageBox.Show("You have unsaved changes. Do you still want to close?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                    return;
                }
            }
        }
        /// <summary>
        /// Select different tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (panelDic.Count != 0)
            {
                panelDic[tabControl1.SelectedTab].GetSelection(out int col, out int row);
                cellNameTextBox.Text = GetCellName(col, row);
                panelDic[tabControl1.SelectedTab].GetValue(col, row, out string val);
                valueTextBox.Text = val;
            }

        }
        /// <summary>
        /// Change background color to Dark tone.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuStrip1.BackColor = SystemColors.ControlDark;
            tabControl1.BackColor = SystemColors.ControlDark;
            tableLayoutPanel1.BackColor = SystemColors.ControlDark;
            tableLayoutPanel2.BackColor = SystemColors.ControlDark;
            tableLayoutPanel3.BackColor = SystemColors.ControlDark;
            tableLayoutPanel4.BackColor = SystemColors.ControlDark;
            contentLabel.BackColor = SystemColors.ControlDark;
            valueLabel.BackColor = SystemColors.ControlDark;
            cellNameLabel.BackColor = SystemColors.ControlDark;
            addSheetButton.BackColor = SystemColors.ControlDarkDark;
            removeSheetButton.BackColor = SystemColors.ControlDarkDark;
        }
        /// <summary>
        /// Change background color to Light tone.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuStrip1.BackColor = SystemColors.Control;
            tabControl1.BackColor = SystemColors.Control;
            tableLayoutPanel1.BackColor = SystemColors.Control;
            tableLayoutPanel2.BackColor = SystemColors.Control;
            tableLayoutPanel3.BackColor = SystemColors.Control;
            tableLayoutPanel4.BackColor = SystemColors.Control;
            contentLabel.BackColor = SystemColors.Control;
            valueLabel.BackColor = SystemColors.Control;
            cellNameLabel.BackColor = SystemColors.Control;
            addSheetButton.BackColor = SystemColors.Control;
            removeSheetButton.BackColor = SystemColors.Control;
        }
        /// <summary>
        /// Change background color to Cool tone.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CoolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color lightBlue = Color.FromArgb(203, 223, 235);
            Color darkBlue = Color.FromArgb(151, 190, 216);

            menuStrip1.BackColor = darkBlue;
            tabControl1.BackColor = lightBlue;
            tableLayoutPanel1.BackColor = lightBlue;
            tableLayoutPanel2.BackColor = lightBlue;
            tableLayoutPanel3.BackColor = lightBlue;
            tableLayoutPanel4.BackColor = lightBlue;
            contentLabel.BackColor = lightBlue;
            valueLabel.BackColor = lightBlue;
            cellNameLabel.BackColor = lightBlue;
            addSheetButton.BackColor = lightBlue;
            removeSheetButton.BackColor = lightBlue;
            addSheetButton.BackColor = darkBlue;
            removeSheetButton.BackColor = darkBlue;
        }
        /// <summary>
        /// Change background color to Warm tone.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WarmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color light = Color.FromArgb(243, 219, 170);
            Color dark = Color.FromArgb(245, 213, 132);

            menuStrip1.BackColor = light;
            tabControl1.BackColor = light;
            tableLayoutPanel1.BackColor = light;
            tableLayoutPanel2.BackColor = light;
            tableLayoutPanel3.BackColor = light;
            tableLayoutPanel4.BackColor = light;
            contentLabel.BackColor = light;
            valueLabel.BackColor = light;
            cellNameLabel.BackColor = light;
            addSheetButton.BackColor = dark;
            removeSheetButton.BackColor = dark;
        }
        /// <summary>
        /// Pop-up the Help menu for instructions on how to use the spreadsheet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "Help\n" +
                "\nFile Menu:\n" +
                "- New: Opens a new spreadsheet window.\n" +
                "- Save: Saves all changes on current spreadsheet.\n" +
                "- Open: Opens spreadsheet from a file.\n" +
                "- Close: Close spreadsheet window.\n" +
                "\nExtra Features: " +
                "\n- Select Background color: options include Dark, Light, Cool, and Warm" +
                "\n- Save as: Saves current spreadsheet to a new file at any directory.\n" +
                "\nHelp Menu:\n" +
                "- Open a help menu to explain the features and instructions on how to use spreadsheet.\n" +
                "\nSpreadsheet features:\n" +
                "- One cell is always highlighted. Cell name and value are displayed on top-left corner.\n" +
                "- Ways to select a cell: 1) use mouse click any cells, 2) type a cell name and use GoToCell button.\n" +
                "- Type in fx text box and press 'Enter' to set the cell contents.\n" +
                "- When a cell is changed, all its dependent cells are also updated.\n" +
                "- New spreadsheet is added to a new tab.\n" +
                "- Can close any selected tabs.\n" +
                "\nSafety features:\n" +
                "- Pop-up dialog to alert user to save changes before closing or prevent unsaved data from being overriden.\n" +
                "\nUses version 'ps6'.\n";


            MessageBox.Show(message);
        }

        /// <summary>
        /// When click, focus and highlight the target cell.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToCellButton_Click(object sender, EventArgs e)
        {
            string cellName = GoToCellTextBox.Text.ToUpper();
            if (cellName == "" || !IsValid(cellName))
            {
                return;
            }
            GetColandRow(cellName, out int col, out int row);

            // If no active spreadsheet, just return
            if (sheetDic.Count == 0)
                return;

            if (IsValid(cellName))
            {
                panelDic[tabControl1.SelectedTab].SetSelection(col, row);
                UpdateSpreadsheet();
            }
        }

        /// <summary>
        /// The cell name to jump to.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToCellTextBox_TextChanged(object sender, KeyPressEventArgs e)
        {
            string cellName = GoToCellTextBox.Text.ToUpper();
            if (cellName == "")
            {
                return;
            }
            if (IsValid(cellName) && e.KeyChar == '\r')
            {
                GoToCellButton_Click(cellName, e);
            }
        }
    }
}
