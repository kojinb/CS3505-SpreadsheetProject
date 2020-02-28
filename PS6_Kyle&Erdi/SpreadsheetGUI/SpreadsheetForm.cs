//Written by Erdi Fan and Kyle Perry

using SS;
using System;
using System.Windows.Forms;
using SpreadsheetGUI;
using System.Text.RegularExpressions;
using SpreadsheetUtilities;
using System.Collections.Generic;
using System.Drawing;

namespace SpreadsheetGUI
{
    public partial class SpreadsheetForm : Form
    {
        private SS.Spreadsheet thisSheet;

        private bool alreadyTriedToClose = false;

        private Color defaultBorderColor;

        private bool isValidDelegate(String name)
        {
            return Regex.IsMatch(name, @"^[a-zA-Z]\d{1,2}$");
        }

        public SpreadsheetForm()
        {
            thisSheet = new Spreadsheet(isValidDelegate, s => s.ToUpper(), "ps6");
            InitializeComponent();
            //set the text box of showing the name of the selected cell to be read only
            selectedCellNameTextBox.ReadOnly = true;
            //set the text box of showing the value of the selected cell to be read only
            selectedCellValueTextBox.ReadOnly = true;
            spreadsheetPanel.SelectionChanged += OnSelectionChanged;
            spreadsheetPanel.GetSelection(out int col, out int row);
            spreadsheetPanel.SetSelection(0, 0);
            defaultBorderColor = spreadsheetPanel.BackColor;
            selectedCellContentsTextBox.Select();
            SetCellNameAndContents();
        }

        /// <summary>
        /// Initialize the apreadsheet
        /// </summary>
        /// <param name="filePath"></param>
        public SpreadsheetForm(String filePath) : this()
        {
            thisSheet = new Spreadsheet(filePath, isValidDelegate, s => s.ToUpper(), "ps6");
        }

        /// <summary>
        /// The change of the selection of cells
        /// </summary>
        /// <param name="ssp"></param>
        private void OnSelectionChanged(SpreadsheetPanel ssp)
        {
            selectedCellContentsTextBox.Clear();
            selectedCellContentsTextBox.Focus();
            SetCellNameAndContents();
        }

        /// <summary>
        /// set the name and the contents of the cell
        /// </summary>
        private void SetCellNameAndContents()
        {
            spreadsheetPanel.GetSelection(out int col, out int row);
            // get and then set the cell name
            selectedCellNameTextBox.Text = "" + (char)(col + 65) + (row + 1);
            object currCellContents = thisSheet.GetCellContents(selectedCellNameTextBox.Text);
            string stringContents;
            if (currCellContents is Formula)
            {
                stringContents = "=" + currCellContents.ToString();
            }
            else
            {
                stringContents = currCellContents.ToString();
            }
            selectedCellContentsTextBox.Text = stringContents;
            spreadsheetPanel.SetValue(col, row, thisSheet.GetCellValue(selectedCellNameTextBox.Text).ToString());
            // get and then set the cell value
            selectedCellValueTextBox.Text = thisSheet.GetCellValue(selectedCellNameTextBox.Text).ToString();

            string cellName = selectedCellNameTextBox.Text;
            string cellContents = selectedCellContentsTextBox.Text;
        }

        // Deals with the New menu
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Tell the application context to run the form on the same thread as the other forms.
            SpreadsheetApplicationContext.getAppContext().RunForm(new SpreadsheetForm());
        }

        // Deals with the Open menu
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if the spreadsheet is changed display a popup
            if (thisSheet.Changed == true)
            {
                //display popup asking to discard or save changes
                String message = "Do you want to save your changes?";
                String caption = "Warning Unsaved Changes";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    //Save changes
                    SaveCurrent();
                }
            }
            // Displays a OpenFileDialog so the user can open the spreadsheet
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Spreadsheets|*.sprd|All Files|*.*";
            ofd.DefaultExt = "sprd";
            ofd.AddExtension = true;
            DialogResult res = ofd.ShowDialog();

            if (res.Equals(DialogResult.OK) && ofd.FileName != "")
            {
                spreadsheetPanel.Clear();
                thisSheet = new Spreadsheet(ofd.FileName, isValidDelegate, s => s.ToUpper(), "ps6");
            }
            if (res.Equals(DialogResult.Cancel)) ofd.Dispose();

            IEnumerable<string> cellsToUpdate = thisSheet.GetNamesOfAllNonemptyCells();
            UpdateCellValues(cellsToUpdate);
            
        }

        // Deals with the Save menu
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveCurrent();
        }

        /// <summary>
        /// Helper method which opens the save dialog and lets the user save the spreadsheet.
        /// </summary>
        private void SaveCurrent()
        {
            // Displays a SaveFileDialog so the user can save the spreadsheet
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Spreadsheets|*.sprd|All Files|*.*";
            sfd.DefaultExt = "sprd";
            sfd.AddExtension = true;
            DialogResult res = sfd.ShowDialog();

            if (res.Equals(DialogResult.OK) && sfd.FileName != "")
            {
                thisSheet.Save(sfd.FileName);
            }
            if (res.Equals(DialogResult.Cancel)) sfd.Dispose();
        }

        /// <summary>
        /// Trigger the events when pressing specific keys happen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectedCellContentsTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //spreadsheetPanel.GetSelection(out int col, out int row);
                if (e.KeyCode == Keys.Enter)
                {
                    IList<String> cellNamesToUpdate = thisSheet.SetContentsOfCell(selectedCellNameTextBox.Text, "d");
                    thisSheet.SetContentsOfCell(selectedCellNameTextBox.Text, selectedCellContentsTextBox.Text);
                    SetCellNameAndContents();
                    UpdateCellValues(cellNamesToUpdate);
                }
            }
            catch (FormulaFormatException)
            {
                String caption = "Invalid Formula";
                String message = "The formula you have entered is invalid";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Helper method to update all of the cells values that are related to the current changed cell.
        /// </summary>
        /// <param name="cellNames"></param>
        private void UpdateCellValues(IEnumerable<String> cellNames)
        {
            foreach (string name in cellNames)
            {
                int column = name.ToCharArray()[0] - 65;
                Int32.TryParse(name.Substring(1), out int rowNum);
                spreadsheetPanel.SetValue(column, --rowNum, thisSheet.GetCellValue(name).ToString());
            }
            spreadsheetPanel.Invalidate();
        }

        /// <summary>
        /// Close by clicking the red X of the window, close the spreadsheet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// help guide of how to change cell selections
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeCellSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String caption = "Change Cell Selection Help";
            String message = "To change which cell is selected you can either click on a new cell or use the arrows on the keyboard.";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// help guide of how to edit the cell 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editCellContentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String caption = "Edit Cell Contents Help";
            String message = "To edit a cell: select the cell you want to edit, enter a number, string, or " +
                             "formula into the cell by typing in the cell contents box (the rightmost box) " +
                             "and pressing enter.";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Help guide info for how to change the selection of a cell.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeCellSelectionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            String caption = "Additional Features Help";
            String message = "To change which cell is selected you can either click on a new cell or use the arrows on the keyboard.";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Help guide info for changing the border color of the spreadsheet form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeSpreadsheetBorderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String caption = "Additional Features Help";
            String message = "To change the border of the spreadsheet select \"Border Color\" from the top bar and choose a color.";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// when click the close in file menu, check if we want to save then close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadsheetForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            //if the spreadsheet is changed display a popup
            if (thisSheet.Changed == true && alreadyTriedToClose == false)
            {
                //display popup asking to discard or save changes
                String message = "Do you want to save your changes?";
                String caption = "Warning Unsaved Changes";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    //Save changes
                    SaveCurrent();
                    Close();
                }
                if (result == DialogResult.No)
                {
                    alreadyTriedToClose = true;
                    e.Cancel = true;
                    Close();
                }
            }
        }

        /// <summary>
        /// additional feature for changing the color of border to blue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BlueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spreadsheetPanel.BackColor = Color.Blue;
            spreadsheetPanel.Invalidate();
        }

        /// <summary>
        /// additional feature for changing the color of border to red
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spreadsheetPanel.BackColor = Color.Red;
            spreadsheetPanel.Invalidate();
        }

        /// <summary>
        /// additional feature for changing the color of border to yellow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YellowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spreadsheetPanel.BackColor = Color.Yellow;
            spreadsheetPanel.Invalidate();
        }

        /// <summary>
        /// additional feature for changing the color of border to green
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spreadsheetPanel.BackColor = Color.Green;
            spreadsheetPanel.Invalidate();
        }

        /// <summary>
        /// additional feature for changing the color of border to grey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GreyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spreadsheetPanel.BackColor = Color.Gray;
            spreadsheetPanel.Invalidate();
        }

        /// <summary>
        /// additional feature for changing the color of border to default
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spreadsheetPanel.BackColor = defaultBorderColor;
            spreadsheetPanel.Invalidate();
        }

        /// <summary>
        /// Overrides the default actions when pressing the up key, down key, left key, and right key.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Down || keyData == Keys.Up || keyData == Keys.Right || keyData == Keys.Left || keyData == Keys.Tab)
            {
                // Process keys
                spreadsheetPanel.GetSelection(out int col, out int row);
                if (keyData == Keys.Up)
                {
                    if (row != 0)
                    {
                        spreadsheetPanel.SetSelection(col, --row);
                        SetCellNameAndContents();
                    }
                }
                if (keyData == Keys.Down)
                {
                    if (row != 98)
                    {
                        spreadsheetPanel.SetSelection(col, ++row);
                        SetCellNameAndContents();
                    }
                }
                if (keyData == Keys.Right)
                {
                    if (col != 25)
                    {
                        spreadsheetPanel.SetSelection(++col, row);
                        SetCellNameAndContents();
                    }
                }
                if (keyData == Keys.Left)
                {
                    if (col != 0)
                    {
                        spreadsheetPanel.SetSelection(--col, row);
                        SetCellNameAndContents();
                    }
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
