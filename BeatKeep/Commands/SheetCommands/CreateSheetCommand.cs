using BeatKeeper.Models;
using BeatKeeper.Services.SheetEditorLoader;

namespace BeatKeeper.Commands
{
    /// <summary>
    /// Class command responsible for creating a new sheet and navigating to sheet editor.
    /// </summary>
    public class CreateSheetCommand : CommandBase
    {
        private readonly ISheetEditorLoader _sheetEditorLoader;

        public CreateSheetCommand(ISheetEditorLoader sheetEditorLoader)
        {
            _sheetEditorLoader = sheetEditorLoader;
        }

        public override void Execute(object parameter)
        {
            Sheet sheet = new("untitled sheet", 60);

            _sheetEditorLoader.LoadSheet(sheet);
        }
    }
}
