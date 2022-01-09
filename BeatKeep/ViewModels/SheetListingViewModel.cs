using BeatKeeper.Commands;
using BeatKeeper.Models;
using BeatKeeper.Services;
using BeatKeeper.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BeatKeeper.ViewModels
{
    public class SheetListingViewModel : ViewModelBase
    {
        private readonly SheetStore _sheetStore;
        private readonly MusicBook _musicBook;
        private readonly INavigationService<SheetEditorViewModel> _navigationService;
        private readonly ObservableCollection<SheetViewModel> _sheets;

        public IEnumerable<SheetViewModel> Sheets => _sheets;
        public ICommand CreateSheet { get; }

        public SheetListingViewModel(SheetStore sheetStore, MusicBook musicBook, INavigationService<SheetEditorViewModel> navigationService)
        {
            _sheetStore = sheetStore;
            _musicBook = musicBook;
            _navigationService = navigationService;
            _sheets = new();

            CreateSheet = new CreateSheetCommand(sheetStore, navigationService);

            RemoveSheetCommand.SheetRemoved += OnRemoveSheet;

            UpdateSheets();
        }

        private void OnRemoveSheet()
        {
            UpdateSheets();
        }
        
        public void UpdateSheets()
        {
            _sheets.Clear();

            foreach (Sheet sheet in _musicBook.GetAllSheets())
            {
                SheetViewModel sheetViewModel = new(sheet, _sheetStore, _musicBook, _navigationService);
                _sheets.Add(sheetViewModel);
            }
        }

        public override void Dispose()
        {
            RemoveSheetCommand.SheetRemoved -= OnRemoveSheet;

            base.Dispose();
        }
    }
}
