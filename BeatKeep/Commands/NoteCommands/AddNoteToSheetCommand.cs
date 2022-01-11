using BeatKeeper.Models;
using BeatKeeper.Stores;
using BeatKeeper.ViewModels;
using System;

namespace BeatKeeper.Commands
{
    /// <summary>
    /// Class command responsible for adding a note to a sheet.
    /// </summary>
    public class AddNoteToSheetCommand : CommandBase
    {
        private readonly SheetStore _sheetStore;
        private readonly TemplateNotesStore _templateNotesStore;

        public static event Action NoteAdded;

        public AddNoteToSheetCommand(SheetStore sheetStore, TemplateNotesStore templateNotesStore)
        {
            _sheetStore = sheetStore;
            _templateNotesStore = templateNotesStore;
        }

        public override void Execute(object parameter)
        {
            Guid id = (Guid)parameter;

            NoteViewModel noteViewModel = _templateNotesStore.GetTemplateNoteById(id);

            Note note = new(noteViewModel.RelativeDuration, noteViewModel.Dots);

            _sheetStore.CurrentSheet.AddNote(note);

            NoteAdded?.Invoke();
        }
    }
}
