using BeatKeeper.Commands;
using BeatKeeper.Models;
using BeatKeeper.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace BeatKeeper.Stores
{
    public class TemplateNotesStore
    {
        private readonly List<NoteViewModel> _templateNotes;

        public IEnumerable<NoteViewModel> TemplateNotes => _templateNotes;

        public TemplateNotesStore()
        {
            _templateNotes = new List<NoteViewModel>();
        }

        public void Load(Sheet sheet)
        {
            _templateNotes.Clear();
            _templateNotes.AddRange(
                new List<NoteViewModel>() {
                    new NoteViewModel(new Note(1, 0), "/Resources/whole-note.png", sheet),
                    new NoteViewModel(new Note(1 / 2f, 0), "/Resources/half-note.png", sheet),
                    new NoteViewModel(new Note(1 / 4f, 0), "/Resources/quarter-note.png", sheet),
                    new NoteViewModel(new Note(1 / 8f, 0), "/Resources/eighth-note.png", sheet)
                });
        }

        public void AddTemplateNote(NoteViewModel templateNote)
        {
            throw new NotImplementedException();
        }
    }
}
