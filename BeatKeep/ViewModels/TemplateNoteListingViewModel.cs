using BeatKeeper.Commands;
using BeatKeeper.Models;
using BeatKeeper.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BeatKeeper.ViewModels
{
    public class TemplateNoteListingViewModel : ViewModelBase
    {
        private readonly TemplateNotesStore _templateNotesStore;
        private readonly ObservableCollection<NoteViewModel> _templateNotes;

        public IEnumerable<NoteViewModel> TemplateNotes => _templateNotes;
        public ICommand CreateNote { get; }
        public ICommand AddNoteCommand { get; }

        public TemplateNoteListingViewModel(Sheet sheet, TemplateNotesStore templateNotesStore)
        {
            _templateNotesStore = templateNotesStore;
            _templateNotes = new();

            AddNoteCommand = new AddNoteToSheetCommand(sheet, templateNotesStore);

            UpdateTemplateNotes();
        }

        public void UpdateTemplateNotes()
        {
            _templateNotes.Clear();

            foreach (NoteViewModel noteViewModel in _templateNotesStore.TemplateNotes)
            {
                _templateNotes.Add(noteViewModel);
            }
        }
    }
}
