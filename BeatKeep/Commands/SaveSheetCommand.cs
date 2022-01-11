using BeatKeeper.Models;
using BeatKeeper.Stores;
using BeatKeeper.ViewModels;
using System.Windows;

namespace BeatKeeper.Commands
{
    public class SaveSheetCommand : CommandBase
    {
        private readonly MusicBook _musicBook;
        private readonly SheetStore _sheetStore;
        private readonly Sheet _sheet;

        public SaveSheetCommand(MusicBook musicBook, SheetStore sheetStore, Sheet sheet)
        {
            _musicBook = musicBook;
            _sheetStore = sheetStore;
            _sheet = sheet;
        }

        public override void Execute(object parameter)
        {
            if (_musicBook.ContainsSheetById(_sheetStore.CurrentSheet.Id))
            {
                _musicBook.RemoveSheetById(_sheetStore.CurrentSheet.Id);
            }

            _sheet.Name = _sheet.Name.Trim();

            _musicBook.AddSheet(_sheet);

            MessageBox.Show("Sheet was successfully saved.", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
