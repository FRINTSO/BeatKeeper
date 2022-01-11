using BeatKeeper.ViewModels;

namespace BeatKeeper.Commands
{
    /// <summary>
    /// Class command responsible for moving an existing note.
    /// </summary>
    public class NoteInsertedCommand : CommandBase
    {
        private readonly SheetMusicViewModel _sheetMusicViewModel;

        public NoteInsertedCommand(SheetMusicViewModel sheetMusicViewModel)
        {
            _sheetMusicViewModel = sheetMusicViewModel;
        }

        public override void Execute(object parameter)
        {
            _sheetMusicViewModel.InsertNote(_sheetMusicViewModel.InsertedNoteViewModel, _sheetMusicViewModel.TargetNoteViewModel);
        }
    }
}
