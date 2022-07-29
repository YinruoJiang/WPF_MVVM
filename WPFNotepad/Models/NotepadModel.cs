/* -- this is a FILEHEADER COMMENT --
	NAME	:	NotepadModel
	PURPOSE :	This class is to get and set the text, filepath and filename and also build a constructor to initialize the filename to "Unknown"
*/
namespace WPFNotepad.Models
{
    public class NotepadModel
    {

        public string Text { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }

        public NotepadModel()
        {
            FileName = "Unknown";
        }
    }
}
