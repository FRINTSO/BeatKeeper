using BeatKeeper.Models;
using BeatKeeper.Stores;
using BeatKeeper.ViewModels;

namespace BeatKeeper.Services.SheetEditorLoader
{
    public class SheetEditorLoader : ISheetEditorLoader
    {
        private readonly SheetStore _sheetStore;
        private readonly INavigationService<SheetEditorViewModel> _navigationService;

        public SheetEditorLoader(SheetStore sheetStore, INavigationService<SheetEditorViewModel> navigationService)
        {
            _sheetStore = sheetStore;
            _navigationService = navigationService;
        }

        public void LoadSheet(Sheet sheet)
        {
            _sheetStore.SavedSheet = sheet;
            _sheetStore.CurrentSheet = sheet.Clone();
            _navigationService.Navigate();
        }
    }
}
