using BeatKeeper.Models;
using System;

namespace BeatKeeper.Commands
{
    public class DeleteNoteFromSheetCommand : CommandBase
    {
        private readonly Sheet _sheet;

        public static event Action NoteRemoved;

        public DeleteNoteFromSheetCommand(Sheet sheet)
        {
            _sheet = sheet;
        }

        public override void Execute(object parameter)
        {
            Guid id = (Guid)parameter;

            _sheet.RemoveNoteById(id);

            NoteRemoved?.Invoke();
        }
    }
}
