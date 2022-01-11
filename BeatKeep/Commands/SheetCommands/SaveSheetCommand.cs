using BeatKeeper.Models;
using BeatKeeper.Stores;
using System.Windows;

namespace BeatKeeper.Commands
{
    public class SaveSheetCommand : CommandBase
    {
        private readonly MusicBook _musicBook;
        private readonly SheetStore _sheetStore;

        public SaveSheetCommand(MusicBook musicBook, SheetStore sheetStore)
        {
            _musicBook = musicBook;
            _sheetStore = sheetStore;
        }

        public override void Execute(object parameter)
        {
            if (_musicBook.ContainsSheetById(_sheetStore.SavedSheet.Id))
            {
                _musicBook.DeleteSheetById(_sheetStore.SavedSheet.Id);
            }

            _sheetStore.CurrentSheet.Name = _sheetStore.CurrentSheet.Name.Trim();

            _musicBook.AddSheet(_sheetStore.CurrentSheet);

            MessageBox.Show("Sheet was successfully saved.", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
