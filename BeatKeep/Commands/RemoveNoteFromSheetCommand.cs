using BeatKeeper.Stores;
using BeatKeeper.ViewModels;
using System;

namespace BeatKeeper.Commands
{
    public class RemoveNoteFromSheetCommand : CommandBase
    {
        private readonly NoteViewModel _noteViewModel;
        private readonly SheetStore _sheetStore;

        public static event Action SheetRemoved;

        public RemoveNoteFromSheetCommand(NoteViewModel noteViewModel, SheetStore sheetStore)
        {
            _noteViewModel = noteViewModel;
            _sheetStore = sheetStore;
        }

        public override void Execute(object parameter)
        {
            _sheetStore.CurrentSheet.RemoveNoteById(_noteViewModel.Id);

            SheetRemoved?.Invoke();
        }
    }
}
