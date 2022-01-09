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

        public TemplateNoteListingViewModel(TemplateNotesStore templateNotesStore)
        {
            _templateNotesStore = templateNotesStore;
            _templateNotes = new();

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
