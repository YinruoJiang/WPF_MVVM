/* -- this is a FILEHEADER COMMENT --
	NAME	:	MainViewModel
	PURPOSE :	This class is to display the notepad main window
*/
using WPFNotepad.Models;

namespace WPFNotepad.ViewModels
{
    public class MainViewModel
    {
        private NotepadModel _notepadModel;
        public FileViewModel File { get; set; }
        public HelpViewModel Help { get; set; }

        public MainViewModel()
        {
            _notepadModel = new NotepadModel();
            Help = new HelpViewModel();           
            File = new FileViewModel(_notepadModel);
        }
    }
}
