using BeatKeeper.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
