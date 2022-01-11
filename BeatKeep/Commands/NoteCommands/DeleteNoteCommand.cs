using BeatKeeper.Models;
using BeatKeeper.Stores;
using System;

namespace BeatKeeper.Commands
{
    /// <summary>
    /// Class command responsible for deleting a note from a music sheet.
    /// </summary>
    public class DeleteNoteCommand : CommandBase
    {
        // TODO: Fix static event

        private readonly SheetStore _sheetStore;

        public static event Action NoteDeleted;

        public DeleteNoteCommand(SheetStore sheetStore)
        {
            _sheetStore = sheetStore;
        }

        public override void Execute(object parameter)
        {
            Guid id = (Guid)parameter;
            _sheetStore.CurrentSheet.DeleteNoteById(id);
            NoteDeleted?.Invoke();
        }
    }
}
