using BeatKeeper.Commands;
using BeatKeeper.Services;
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
        public ICommand AddTemplateNote { get; }

        public TemplateNoteListingViewModel(TemplateNotesStore templateNotesStore, INavigationService<AddTemplateNoteViewModel> modalNavigationService)
        {
            _templateNotesStore = templateNotesStore;
            _templateNotes = new();

            AddTemplateNote = new AddTemplateNoteCommand(modalNavigationService);

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
