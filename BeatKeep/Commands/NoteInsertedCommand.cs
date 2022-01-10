using BeatKeeper.ViewModels;

namespace BeatKeeper.Commands
{
    public class NoteInsertedCommand : CommandBase
    {
        private readonly SheetNoteViewModel _sheetNoteViewModel;

        public NoteInsertedCommand(SheetNoteViewModel sheetNoteViewModel)
        {
            _sheetNoteViewModel = sheetNoteViewModel;
        }

        public override void Execute(object parameter)
        {
            _sheetNoteViewModel.InsertNote(_sheetNoteViewModel.InsertedNoteViewModel, _sheetNoteViewModel.TargetNoteViewModel);
        }
    }
}
