using BeatKeeper.Models;
using BeatKeeper.Stores;
using BeatKeeper.ViewModels;
using System.Windows;

namespace BeatKeeper.Commands
{
    public class SaveSheetCommand : CommandBase
    {
        private readonly SheetEditorViewModel _sheetEditorViewModel;
        private readonly MusicBook _musicBook;
        private readonly SheetStore _sheetStore;

        public SaveSheetCommand(SheetEditorViewModel sheetEditorViewModel, MusicBook musicBook, SheetStore sheetStore)
        {
            _sheetEditorViewModel = sheetEditorViewModel;
            _musicBook = musicBook;
            _sheetStore = sheetStore;
        }

        public override void Execute(object parameter)
        {
            if (!_musicBook.ContainsSheetById(_sheetStore.CurrentSheet.Id))
            {
                _musicBook.AddSheet(_sheetStore.CurrentSheet);
            }

            _sheetStore.CurrentSheet.Name = _sheetEditorViewModel.Name.Trim();
            _sheetStore.CurrentSheet.BeatsPerMinute = _sheetEditorViewModel.BeatsPerMinute;

            MessageBox.Show("Sheet was successfully saved.", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
