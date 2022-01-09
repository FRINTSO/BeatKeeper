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
        private readonly SheetStore _sheetStore;

        public static event Action SheetAdded;

        public AddNoteToSheetCommand(NoteViewModel noteViewModel, SheetStore sheetStore)
        {
            _noteViewModel = noteViewModel;
            _sheetStore = sheetStore;
        }

        public override void Execute(object parameter)
        {
            // Add a note to the current sheet!?!?!?!?!??! Sheet Store has the current sheet

            Note note = new(_noteViewModel.RelativeDuration, _noteViewModel.Dots);

            _sheetStore.CurrentSheet.AddNote(note);

            SheetAdded?.Invoke();
        }
    }
}
