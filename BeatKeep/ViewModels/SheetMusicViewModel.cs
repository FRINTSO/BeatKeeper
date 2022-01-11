using BeatKeeper.Commands;
using BeatKeeper.Models;
using BeatKeeper.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace BeatKeeper.ViewModels
{
    public class SheetMusicViewModel : ViewModelBase
    {
        private readonly Sheet _sheet;
        private readonly TemplateNotesStore _templateNotesStore;
        private readonly ObservableCollection<NoteViewModel> _notes;
        private NoteViewModel _incomingNoteViewModel;
        private NoteViewModel _insertedNoteViewModel;
        private NoteViewModel _targetNoteViewModel;

        public SheetMusicViewModel(SheetStore sheetStore, TemplateNotesStore templateNotesStore)
        {
            _sheetStore = sheetStore;
            _templateNotesStore = templateNotesStore;
            _notes = new();

            NoteReceivedCommand = new NoteReceivedCommand(this);
            NoteInsertedCommand = new NoteInsertedCommand(this);

            AddNoteToSheetCommand.SheetAdded += UpdateNotes;
            DeleteNoteCommand.SheetRemoved += UpdateNotes;

            UpdateNotes();
        }

        public IEnumerable<NoteViewModel> Notes => _notes;

        public NoteViewModel IncomingNoteViewModel
        {
            get => _incomingNoteViewModel;
            set
            {
                _incomingNoteViewModel = value;
                OnPropertyChanged(nameof(IncomingNoteViewModel));
            }
        }

        public NoteViewModel InsertedNoteViewModel
        {
            get => _insertedNoteViewModel;
            set
            {
                _insertedNoteViewModel = value;
                OnPropertyChanged(nameof(InsertedNoteViewModel));
            }
        }

        public NoteViewModel TargetNoteViewModel
        {
            get => _targetNoteViewModel;
            set
            {
                _targetNoteViewModel = value;
                OnPropertyChanged(nameof(TargetNoteViewModel));
            }
        }

        public ICommand NoteReceivedCommand { get; }
        public ICommand NoteInsertedCommand { get; }
        public ICommand DeleteNoteCommand { get; }


        public void AddNote(NoteViewModel noteViewModel)
        {
            Note note = new(noteViewModel.RelativeDuration, noteViewModel.Dots);

            noteViewModel = new(note, noteViewModel.NoteImageSource);

            _sheet.AddNote(note);
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
                _sheet.MoveNotes(oldIndex, nextIndex);
                _notes.Move(oldIndex, nextIndex);
            }
        }

        public void UpdateNotes()
        {
            _notes.Clear();

            foreach (Note note in _sheet.GetAllNotes())
            {
                // TODO: Resolve image file from context of added note

                string noteImageSource = _templateNotesStore.TemplateNotes.First(templateNote => templateNote.RelativeDuration == note.RelativeDuration).NoteImageSource;

                NoteViewModel noteViewModel = new(note, noteImageSource);
                _notes.Add(noteViewModel);
            }
        }

        public override void Dispose()
        {
            AddNoteToSheetCommand.SheetAdded -= UpdateNotes;
            DeleteNoteCommand.SheetRemoved -= UpdateNotes;

            base.Dispose();
        }
    }
}
