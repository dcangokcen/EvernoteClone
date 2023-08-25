﻿using EvernoteClone_Demo.Models;
using EvernoteClone_Demo.ViewModel.Commands;
using EvernoteClone_Demo.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvernoteClone_Demo.ViewModel
{
    public class NotesVM : INotifyPropertyChanged
    {
        public ObservableCollection<Notebook> Notebooks{ get; set; }

		private Notebook selectedNotebook;

		public Notebook SelectedNotebook
        {
			get { return selectedNotebook; }
			set 
			{ 
				selectedNotebook = value;
                OnPropertyChanged("SelectedNotebook");
				GetNotes();
			}
		}

        public ObservableCollection<Note> Notes{ get; set; }

        public  NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }
		public event PropertyChangedEventHandler? PropertyChanged;


		public NotesVM()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            GetNotebooks();
            
        }

        public void CreateNotebook()
        {
            Notebook newNotebook = new Notebook() 
            {
                Name = "Notebook",

            };

            DatabaseHelper.Insert(newNotebook);
            GetNotebooks();
        }
        public void CreateNote(int notebookId)
        {
            Note newNote = new Note() 
            { 
                NotebookId = notebookId,
                CreateAtTime = DateTime.Now,
                UpdateAtTime = DateTime.Now,
                Title = $"Note for {DateTime.Now.ToString()}"
            };

            DatabaseHelper.Insert(newNote);
            GetNotes();
        }

        private void GetNotebooks()
        {
            var notebooks = DatabaseHelper.Read<Notebook>();

            Notebooks.Clear();
            foreach (Notebook notebook in notebooks)
            {
                Notebooks.Add(notebook);
            }
        }

		private void GetNotes()
		{
            if (SelectedNotebook != null)
            {
				var notes = DatabaseHelper.Read<Note>().Where(n => n.NotebookId == SelectedNotebook.Id).ToList();

				Notes.Clear();
				foreach (Note note in notes)
				{
					Notes.Add(note);
				}
			}
		}

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

	}
}