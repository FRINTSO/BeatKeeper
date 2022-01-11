using BeatKeeper.Stores;
using BeatKeeper.ViewModels;
using System;

namespace BeatKeeper.Commands
{
    /// <summary>
    /// Class command responsible for deleting a note from a music sheet.
    /// </summary>
    public class DeleteNoteCommand : CommandBase
    {
        // TODO: Fix static event

        private readonly NoteViewModel _noteViewModel;
        private readonly SheetStore _sheetStore;

        public static event Action SheetRemoved;

        public DeleteNoteCommand(NoteViewModel noteViewModel, SheetStore sheetStore)
        {
            _noteViewModel = noteViewModel;
            _sheetStore = sheetStore;
        }

        public override void Execute(object parameter)
        {
            _sheetStore.CurrentSheet.DeleteNoteById(_noteViewModel.Id);

            SheetRemoved?.Invoke();
        }
    }
}
