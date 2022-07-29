/*
* FILE : WPFNotepad
* PROJECT : PROG2121 - Assignment #2
* PROGRAMMER : Yinruo Jiang
* FIRST VERSION : 2021-09-25
* SECOND VERSION : 2021-11-03
* DESCRIPTION : The functions in this file are used to mimic the notepad in Windows
*/
using System.Windows;

namespace WPFNotepad
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        // FUNCTION : txtEditor_SelectionChanged
        // DESCRIPTION :This function is to count the character length in the textbox, and display in the status bar in the buttom
        private void txtEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {

            int count = txtEditor.Text.Length;
            lblCursorPosition.Text = count.ToString();
        }
    }
}
