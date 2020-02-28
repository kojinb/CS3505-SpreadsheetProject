This assignment is written by Kyle Perry and Erdi Fan.

10/1/19
Design Decisions
	1 - Setup an instance variable for our dependency graph.
10/2/19
Design Decisions
	1 - Excel doesn't have a calculate button so we removed it and set an event listener for keydown
		when the enter key is pressed while in the contents textbox the contents is put into the cell.
Problems
	2 - Need to add an error popup if you try enter an invalid formula into a cell (Solved added message box)
	3 - When we open an existing file we need to loop through all of the non empty cells of the
		spreadsheet and populate the associated cells in the spreadsheet form.
	4 - When we change the contents of a cell we need to get the updated value from the
		spreadsheet and put it in the associated place in the spreadsheet form
	5 - When we click file open and then hit cancel or the X a second file dialog window opens,
		should just be the first one (Solved, we were calling ShowDialog() twice)
10/3/19
Design Decisions
	1 - Implemented a solution for problem 4 from above. Used a helper method.
Problems
	1 - When opening a spreadsheet from a file it is loading correctly and creating a spreadsheet object
		but the values do not display on the spreadsheet form gui unless you click on the cell (Solved)
	2 - When opening an existing spreadsheet all cell values are being treated as a formula and have
		an "=" prepended to them when putting the contents into the setcellcontents box (Solved this was not actually a bug just a misunderstanding of our test.)
10/4/19
Design Decisions
	1 - 
Problems
	1 - Need to figure out how to initialize a click in cell A1 when the spreadsheet is first opened. 
		Researched for more than an hour and talked with a TA who was unable to help us. Further research
		on Piazza shows a question similar to this that was answered and provided with a solution to implement this.
		(Solved)
	2 - When you put a formula in a cell referencing a cell with a value and then you delete the value
		from the cell it is referencing the dependent cell is not updating. (Solved)
	3 - When you use the arrow keys to move between cells it also moves through the contents text box.
		(Solved)
Additional Features
	1 - Be able to use the arrow keys to move to different cells. (Implemented)
	2 - Change the border color of the spreadsheet. 