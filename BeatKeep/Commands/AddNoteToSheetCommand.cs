using BeatKeeper.Models;
using BeatKeeper.Stores;
using BeatKeeper.ViewModels;
using Microsoft.Extensions.Logging;
using System;

namespace BeatKeeper.Commands
{
    public class AddNoteToSheetCommand : CommandBase
    {
        private readonly NoteViewModel _noteViewModel;
        private readonly Sheet _sheet;

        public static event Action NoteAdded;

        public AddNoteToSheetCommand(NoteViewModel noteViewModel, Sheet sheet)
        {
            _noteViewModel = noteViewModel;
            _sheet = sheet;
        }

        public override void Execute(object parameter)
        {
            Note note = new(_noteViewModel.RelativeDuration, _noteViewModel.Dots);

            _sheet.AddNote(note);

            NoteAdded?.Invoke();
        }
    }
}
