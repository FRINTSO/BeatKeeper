using BeatKeeper.Models;
using BeatKeeper.ViewModels;
using System;

namespace BeatKeeper.Commands
{
    // TODO: Fix static event

    /// <summary>
    /// Class command for deleting a sheet from the music book.
    /// </summary>
    public class DeleteSheetCommand : CommandBase
    {
        private readonly SheetViewModel _sheetViewModel;
        private readonly MusicBook _musicBook;

        public static event Action SheetDeleted;

        public DeleteSheetCommand(SheetViewModel sheetViewModel, MusicBook musicBook)
        {
            _sheetViewModel = sheetViewModel;
            _musicBook = musicBook;
        }

        public override void Execute(object parameter)
        {
            _musicBook.DeleteSheetById(_sheetViewModel.Id);
            SheetDeleted?.Invoke();
        }
    }
}
