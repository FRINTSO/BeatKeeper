using BeatKeeper.Models;
using BeatKeeper.ViewModels;
using System;

namespace BeatKeeper.Commands
{
    public class RemoveSheetCommand : CommandBase
    {
        private readonly SheetViewModel _sheetViewModel;
        private readonly MusicBook _musicBook;

        public static event Action SheetRemoved;

        public RemoveSheetCommand(SheetViewModel sheetViewModel, MusicBook musicBook)
        {
            _sheetViewModel = sheetViewModel;
            _musicBook = musicBook;
        }

        public override void Execute(object parameter)
        {
            _musicBook.RemoveSheetById(_sheetViewModel.Id);
            SheetRemoved?.Invoke();
        }
    }
}
