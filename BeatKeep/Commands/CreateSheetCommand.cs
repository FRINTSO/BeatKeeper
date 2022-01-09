using BeatKeeper.Models;
using BeatKeeper.Services;
using BeatKeeper.Stores;
using BeatKeeper.ViewModels;

namespace BeatKeeper.Commands
{
    public class CreateSheetCommand : CommandBase
    {
        private readonly SheetStore _sheetStore;
        private readonly INavigationService<SheetEditorViewModel> _navigateService;

        public CreateSheetCommand(SheetStore sheetStore, INavigationService<SheetEditorViewModel> navigateService)
        {
            _sheetStore = sheetStore;
            _navigateService = navigateService;
        }

        public override void Execute(object parameter)
        {
            // Create Sheet
            Sheet sheet = new("untitled sheet", 60);

            _sheetStore.CurrentSheet = sheet;

            // Navigate to sheet editor and pass created sheet
            _navigateService.Navigate();

        }
    }
}
