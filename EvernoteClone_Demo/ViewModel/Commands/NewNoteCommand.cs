using EvernoteClone_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EvernoteClone_Demo.ViewModel.Commands
{
    public class NewNoteCommand : ICommand
    {
        public NotesVM VM { get; set; }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public NewNoteCommand(NotesVM vm)
        {
            VM = vm;
        }
        public bool CanExecute(object? parameter)
        {
            Notebook selectedNotebook = parameter as Notebook;

            return selectedNotebook != null ? true : false;
        }

        public void Execute(object? parameter)
        {
            //TODO: Create 
            Notebook selectedNotebook = parameter as Notebook;
            VM.CreateNote(selectedNotebook.Id);
        }
    }
}
