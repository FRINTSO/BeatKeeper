using BeatKeeper.Models;
using BeatKeeper.Services.SheetEditorLoader;
using BeatKeeper.ViewModels;

namespace BeatKeeper.Commands
{
    public class OpenSheetCommand : CommandBase
    {
        private readonly SheetViewModel _sheetViewModel;
        private readonly MusicBook _musicBook;
        private readonly ISheetEditorLoader _sheetEditorLoader;

        public OpenSheetCommand(SheetViewModel sheetViewModel, MusicBook musicBook, ISheetEditorLoader sheetEditorLoader)
        {
            _sheetViewModel = sheetViewModel;
            _musicBook = musicBook;
            _sheetEditorLoader = sheetEditorLoader;
        }

        public override void Execute(object parameter)
        {
            Sheet sheet = _musicBook.GetSheetById(_sheetViewModel.Id);

            _sheetEditorLoader.LoadSheet(sheet);
        }
    }
}
