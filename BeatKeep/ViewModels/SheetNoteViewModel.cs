﻿using BeatKeeper.Commands;
using BeatKeeper.Models;
using BeatKeeper.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BeatKeeper.ViewModels
{
    public class SheetNoteViewModel : ViewModelBase
    {
        private readonly SheetStore _sheetStore;
        private readonly TemplateNotesStore _templateNotesStore;
        private readonly ObservableCollection<NoteViewModel> _notes;

        public IEnumerable<NoteViewModel> Notes => _notes;

        private NoteViewModel _incomingNoteViewModel;
        public NoteViewModel IncomingNoteViewModel
        {
            get
            {
                return _incomingNoteViewModel;
            }
            set
            {
                _incomingNoteViewModel = value;
                OnPropertyChanged(nameof(IncomingNoteViewModel));
            }
        }

        private NoteViewModel _insertedNoteViewModel;
        public NoteViewModel InsertedNoteViewModel
        {
            get
            {
                return _insertedNoteViewModel;
            }
            set
            {
                _insertedNoteViewModel = value;
                OnPropertyChanged(nameof(InsertedNoteViewModel));
            }
        }

        private NoteViewModel _targetNoteViewModel;
        public NoteViewModel TargetNoteViewModel
        {
            get
            {
                return _targetNoteViewModel;
            }
            set
            {
                _targetNoteViewModel = value;
                OnPropertyChanged(nameof(TargetNoteViewModel));
            }
        }

        public ICommand NoteReceivedCommand { get; }
        public ICommand NoteInsertedCommand { get; }

        public SheetNoteViewModel(SheetStore sheetStore, TemplateNotesStore templateNotesStore)
        {
            _sheetStore = sheetStore;
            _templateNotesStore = templateNotesStore;
            _notes = new();

            NoteReceivedCommand = new NoteReceivedCommand(this);
            NoteInsertedCommand = new NoteInsertedCommand(this);

            AddNoteToSheetCommand.SheetAdded += UpdateNotes;
            RemoveNoteFromSheetCommand.SheetRemoved += UpdateNotes;

            UpdateNotes();
        }

        public void AddNote(NoteViewModel noteViewModel)
        {
            Note note = new(noteViewModel.RelativeDuration, noteViewModel.Dots);

            noteViewModel = new(note, noteViewModel.NoteImageSource, _sheetStore);

            _sheetStore.CurrentSheet.AddNote(note);
            _notes.Add(noteViewModel);
        }

        public void InsertNote(NoteViewModel insertedNote, NoteViewModel targetNote)
        {
            if (insertedNote == targetNote)
            {
                return;
            }

            int oldIndex = _notes.IndexOf(insertedNote);
            int nextIndex = _notes.IndexOf(targetNote);

            if (oldIndex != -1 && nextIndex != -1)
            {
                _sheetStore.CurrentSheet.MoveNotes(oldIndex, nextIndex);
                _notes.Move(oldIndex, nextIndex);
            }
        }

        public void UpdateNotes()
        {
            _notes.Clear();

            foreach (Note note in _sheetStore.CurrentSheet.GetAllNotes())
            {
                // TODO: Resolve image file from context of added note

                var noteImageSource = _templateNotesStore.TemplateNotes.First(templateNote => templateNote.RelativeDuration == note.RelativeDuration).NoteImageSource;

                NoteViewModel noteViewModel = new(note, noteImageSource, _sheetStore);
                _notes.Add(noteViewModel);
            }
        }

        public override void Dispose()
        {
            AddNoteToSheetCommand.SheetAdded -= UpdateNotes;
            RemoveNoteFromSheetCommand.SheetRemoved -= UpdateNotes;

            base.Dispose();
        }
    }
}
