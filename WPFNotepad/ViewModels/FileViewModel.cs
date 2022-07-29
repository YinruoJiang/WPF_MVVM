/* -- this is a FILEHEADER COMMENT --
	NAME	:	FileViewModel
	PURPOSE :	This class is to set up the File window in the notepad
*/
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WPFNotepad.Models;

namespace WPFNotepad.ViewModels
{
    public class FileViewModel : INotifyPropertyChanged
    {
        private NotepadModel _notepad;
        public string Text
        {
            get { return _notepad.Text; }
            set
            {
                if (value == _notepad.Text)
                    return;

                _notepad.Text = value;
                isChanged = true;

                OnPropertyChanged();
            }
        }

        public string FilePath
        {
            get { return _notepad.FilePath; }
            set
            {
                if (value == _notepad.FilePath)
                    return;

                _notepad.FilePath = value;

                OnPropertyChanged();
            }
        }

        public string FileName
        {
            get { return _notepad.FileName; }
            set
            {
                if (value == _notepad.FileName)
                    return;

                _notepad.FileName = value;

                OnPropertyChanged();
            }
        }

        public bool isChanged { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        // FUNCTION : OnPropertyChanged
        // DESCRIPTION :This function is to check the property changed
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        public ICommand NewCommand { get; }
        public ICommand SaveAsCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand CloseCommand { get; }

       

        public FileViewModel(NotepadModel notepadModel)
        {
            _notepad = notepadModel;
            NewCommand = new RelayCommand(param => NewFile());
            SaveAsCommand = new RelayCommand(param => SaveFileAs());
            OpenCommand = new RelayCommand(param => OpenFile());
            CloseCommand = new RelayCommand(param => CloseFile((Window)param));
        }

        // FUNCTION : NewFile
        // DESCRIPTION :This function is an action which is mimic the new button in the notepad
        public void NewFile()
        {
            if (isChanged == true && string.IsNullOrEmpty(Text)==false)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save the changes to " + FileName, "Notepad", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                {
                    SaveHandler();
                    FileName = string.Empty;
                    FilePath = string.Empty;
                    Text = string.Empty;
                }
                else if (result == MessageBoxResult.No)
                {
                    FileName = string.Empty;
                    FilePath = string.Empty;
                    Text = string.Empty;
                }
            }
            else
            {
                FileName = string.Empty;
                FilePath = string.Empty;
                Text = string.Empty;
            }           
        }

        // FUNCTION : OpenFile
        // DESCRIPTION :This function is an action which is mimic the open button in the notepad
        private void OpenFile()
        {
            if (isChanged == true && string.IsNullOrEmpty(Text)==false)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save the changes to " + FileName, "Notepad", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                {
                    SaveHandler();
                    OpenHandler();
                }
                else if (result == MessageBoxResult.No)
                {
                    FileName = string.Empty;
                    FilePath = string.Empty;
                    Text = string.Empty;
                    OpenHandler();
                }
            }
            else
            {
                OpenHandler();
            }
        }

        // FUNCTION : SaveFileAs
        // DESCRIPTION :This function is an action which is mimic the Save as button in the notepad
        private void SaveFileAs()
        {
            SaveHandler();
        }

        // FUNCTION : CloseFile
        // DESCRIPTION :This function is an action which is mimic the Close button in the notepad
        private void CloseFile(Window window)
        {
            if (isChanged == true && string.IsNullOrEmpty(Text)==false)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save the changes to " + FileName, "Notepad", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                {
                    SaveHandler();
                    if (window != null)
                    {
                        window.Close();
                    }
                }
                else if (result == MessageBoxResult.No)
                {
                    if (window != null)
                    {
                        window.Close();
                    }
                }
            }
            else
            {
                if (window != null)
                {
                    window.Close();
                }
            }
        }

        // FUNCTION : DockFile
        // DESCRIPTION :This template is to set teh filepath adn filename
        private void DockFile<T>(T dialog) where T : FileDialog
        {
            FilePath = dialog.FileName;
            FileName = dialog.SafeFileName;
        }

        // FUNCTION : SaveHandler
        // DESCRIPTION :This function is to display save file dialog in the notepad
        private void SaveHandler()
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
                FilePath = saveFileDialog.FileName;
            try
            {
                File.WriteAllText(saveFileDialog.FileName, Text);
            }
            catch (System.ArgumentException)
            {
                Console.WriteLine("Please provide a name for this file");
            }
                
            DockFile(saveFileDialog);
            isChanged = false;
        }

        // FUNCTION : OpenHandler
        // DESCRIPTION :This function is to display open file dialog in the notepad
        private void OpenHandler()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text file (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                DockFile(openFileDialog);
                FilePath = openFileDialog.FileName;
                Text = File.ReadAllText(openFileDialog.FileName);
            }
            DockFile(openFileDialog);
            isChanged = false;
        }
    }
}
