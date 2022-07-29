/* -- this is a FILEHEADER COMMENT --
	NAME	:	HelpViewModel
	PURPOSE :	This class is to set up the about window in the notepad
*/
using System.Windows.Input;

namespace WPFNotepad.ViewModels
{
    public class HelpViewModel
    {
        public ICommand HelpCommand { get; }

        public HelpViewModel()
        {
            HelpCommand = new RelayCommand(param => DisplayAbout());
        }

        // FUNCTION : DisplayAbout
        // DESCRIPTION :This function is to display help dialog in the notepad
        private void DisplayAbout()
        {
            HelpDialog helpDialog = new HelpDialog();
            helpDialog.ShowDialog();
        }
    }
}
