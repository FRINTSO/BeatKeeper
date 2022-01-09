using BeatKeeper.Commands;
using BeatKeeper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BeatKeeper.ViewModels
{
    public class AddTemplateNoteViewModel : ViewModelBase
    {
        private readonly INavigationService<SheetEditorViewModel> _closeModalNavigationService;

        private string _templateNoteImageSource;
        public string TemplateNoteImageSource
        {
            get
            {
                return _templateNoteImageSource;
            }
            set
            {
                _templateNoteImageSource = value;
                OnPropertyChanged(nameof(TemplateNoteImageSource));
            }
        }

        public ICommand BrowseImages { get; }
        public ICommand CreateTemplateNote { get; }
        public ICommand Cancel { get; }

        public AddTemplateNoteViewModel(INavigationService<SheetEditorViewModel> closeModalNavigationService)
        {
            _closeModalNavigationService = closeModalNavigationService;

            BrowseImages = new BrowseImagesCommand(this);
            CreateTemplateNote = new CreateTemplateNoteCommand();
            Cancel = new CloseModalNavigationCommand();
        }
    }
}
