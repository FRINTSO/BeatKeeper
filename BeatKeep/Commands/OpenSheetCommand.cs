using BeatKeeper.Models;
using BeatKeeper.Services;
using BeatKeeper.Stores;
using BeatKeeper.ViewModels;

namespace BeatKeeper.Commands
{
    public class OpenSheetCommand : CommandBase
    {
        private readonly SheetViewModel _sheetViewModel;
        private readonly SheetStore _sheetStore;
        private readonly MusicBook _musicBook;
        private readonly INavigationService<SheetEditorViewModel> _navigationService;

        public OpenSheetCommand(SheetViewModel sheetViewModel, SheetStore sheetStore, MusicBook musicBook, INavigationService<SheetEditorViewModel> navigationService)
        {
            _sheetViewModel = sheetViewModel;
            _sheetStore = sheetStore;
            _musicBook = musicBook;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            // Load SheetEditorView and load sheetViewModel
            Sheet sheet = _musicBook.GetSheetById(_sheetViewModel.Id);

            _sheetStore.CurrentSheet = sheet;

            _navigationService.Navigate();
        }
    }
}
