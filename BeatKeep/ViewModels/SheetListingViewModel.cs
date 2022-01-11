using BeatKeeper.Commands;
using BeatKeeper.Models;
using BeatKeeper.Services.SheetEditorLoader;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BeatKeeper.ViewModels
{
    public class SheetListingViewModel : ViewModelBase
    {
        private readonly MusicBook _musicBook;
        private readonly ISheetEditorLoader _sheetEditorLoader;
        private readonly ObservableCollection<SheetViewModel> _sheets;

        public SheetListingViewModel(MusicBook musicBook, ISheetEditorLoader sheetEditorLoader)
        {
            _musicBook = musicBook;
            _sheetEditorLoader = sheetEditorLoader;
            _sheets = new();

            CreateSheet = new CreateSheetCommand(sheetEditorLoader);

            DeleteSheetCommand.SheetDeleted += UpdateSheets;

            UpdateSheets();
        }

        public IEnumerable<SheetViewModel> Sheets => _sheets;
        public ICommand CreateSheet { get; }

        public void UpdateSheets()
        {
            _sheets.Clear();

            foreach (Sheet sheet in _musicBook.GetAllSheets())
            {
                SheetViewModel sheetViewModel = new(sheet, _musicBook, _sheetEditorLoader);
                _sheets.Add(sheetViewModel);
            }
        }

        public override void Dispose()
        {
            DeleteSheetCommand.SheetDeleted -= UpdateSheets;

            base.Dispose();
        }
    }
}
