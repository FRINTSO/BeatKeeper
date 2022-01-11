using BeatKeeper.ViewModels;

namespace BeatKeeper.Commands
{
    /// <summary>
    /// Class command for a note dropped in the sheet editor.
    /// </summary>
    public class NoteReceivedCommand : CommandBase
    {
        private readonly SheetMusicViewModel _sheetNoteViewModel;

        public NoteReceivedCommand(SheetMusicViewModel sheetNoteViewModel)
        {
            _sheetNoteViewModel = sheetNoteViewModel;
        }

        public override void Execute(object parameter)
        {
            _sheetNoteViewModel.AddNote(_sheetNoteViewModel.IncomingNoteViewModel);
        }
    }
}
