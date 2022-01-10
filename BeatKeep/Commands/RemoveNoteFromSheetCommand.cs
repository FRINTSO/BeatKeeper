using BeatKeeper.Models;
using BeatKeeper.ViewModels;
using System;

namespace BeatKeeper.Commands
{
    public class RemoveNoteFromSheetCommand : CommandBase
    {
        private readonly NoteViewModel _noteViewModel;
        private readonly Sheet _sheet;

        public static event Action NoteRemoved;

        public RemoveNoteFromSheetCommand(NoteViewModel noteViewModel, Sheet sheet)
        {
            _noteViewModel = noteViewModel;
            _sheet = sheet;
        }

        public override void Execute(object parameter)
        {
            _sheet.RemoveNoteById(_noteViewModel.Id);

            NoteRemoved?.Invoke();
        }
    }
}
