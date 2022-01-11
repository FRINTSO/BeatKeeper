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
        private readonly Sheet _sheet;
        private readonly TemplateNotesStore _templateNotesStore;

        public static event Action NoteAdded;

        public AddNoteToSheetCommand(Sheet sheet, TemplateNotesStore templateNotesStore)
        {
            _sheet = sheet;
            _templateNotesStore = templateNotesStore;
        }

        public override void Execute(object parameter)
        {
            Guid id = (Guid)parameter;

            NoteViewModel noteViewModel = _templateNotesStore.GetTemplateNoteById(id);

            Note note = new(noteViewModel.RelativeDuration, noteViewModel.Dots);

            _sheet.AddNote(note);

            NoteAdded?.Invoke();
        }
    }
}
