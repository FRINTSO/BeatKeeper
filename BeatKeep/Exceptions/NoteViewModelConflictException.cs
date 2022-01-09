using BeatKeeper.Models;
using BeatKeeper.ViewModels;
using System;

namespace BeatKeeper.Exceptions
{
    public class NoteViewModelConflictException : Exception
    {
        public NoteViewModel IncomingNoteViewModel { get; }

        public NoteViewModelConflictException(NoteViewModel incomingNoteViewModel)
        {
            IncomingNoteViewModel = incomingNoteViewModel;
        }

        public NoteViewModelConflictException(string message, NoteViewModel incomingNoteViewModel) : base(message)
        {
            IncomingNoteViewModel = incomingNoteViewModel;
        }

        public NoteViewModelConflictException(string message, Exception innerException, NoteViewModel incomingNoteViewModel) : base(message, innerException)
        {
            IncomingNoteViewModel = incomingNoteViewModel;
        }
    }
}
