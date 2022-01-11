using BeatKeeper.Models;
using BeatKeeper.Stores;
using BeatKeeper.ViewModels;
using System;

namespace BeatKeeper.Commands
{
    /// <summary>
    /// Class command responsible for adding a note to a sheet.
    /// </summary>
    public class AddNoteToSheetCommand : CommandBase
    {
        // TODO: Fix static event

        private readonly NoteViewModel _noteViewModel;  // NoteViewModel containing information about the note that will be added
        private readonly SheetStore _sheetStore;  // SheetStore containing the current sheet

        public static event Action SheetAdded;

        public AddNoteToSheetCommand(NoteViewModel noteViewModel, SheetStore sheetStore)
        {
            _noteViewModel = noteViewModel;
            _sheetStore = sheetStore;
        }

        public override void Execute(object parameter)
        {
            Note note = new(_noteViewModel.RelativeDuration, _noteViewModel.Dots);


            // BUG: Adds note to the current sheet, making saving pointless, since it saves automatically by writing to source
            _sheetStore.CurrentSheet.AddNote(note);

            SheetAdded?.Invoke();
        }
    }
}
