using BeatKeeper.Models;
using System;

namespace BeatKeeper.ViewModels
{
    public class NoteViewModel : ViewModelBase
    {
        private readonly Note _note;

        public NoteViewModel(Note note, string noteImageSource)
        {
            _note = note;
            NoteImageSource = noteImageSource;
        }

        public Guid Id => _note.Id;
        public double RelativeDuration => _note.RelativeDuration;
        public byte Dots => _note.Dots;
        public double Duration => _note.Duration;
        public string NoteImageSource { get; }
    }
}
