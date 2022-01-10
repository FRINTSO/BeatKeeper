using BeatKeeper.Commands;
using BeatKeeper.Models;
using BeatKeeper.Stores;
using System;
using System.Windows.Input;

namespace BeatKeeper.ViewModels
{
    public class NoteViewModel : ViewModelBase
    {
        private readonly Note _note;

        public Guid Id => _note.Id;
        public double RelativeDuration => _note.RelativeDuration;
        public byte Dots => _note.Dots;
        public double Duration => _note.Duration;

        public string NoteImageSource { get; }
        public ICommand AddNote { get; }
        public ICommand RemoveNote { get; }

        public NoteViewModel(Note note, string noteImageSource, Sheet sheet)
        {
            _note = note;
            NoteImageSource = noteImageSource;

            AddNote = new AddNoteToSheetCommand(this, sheet);
            RemoveNote = new RemoveNoteFromSheetCommand(this, sheet);
        }
    }
}
