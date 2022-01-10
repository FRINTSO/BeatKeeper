using BeatKeeper.ViewModels;
using System.Collections.Generic;

namespace BeatKeeper.Services.TemplateNoteCreators
{
    public interface ITemplateNoteProvider
    {
        IEnumerable<NoteViewModel> GetAllTemplateNotes();
    }
}
