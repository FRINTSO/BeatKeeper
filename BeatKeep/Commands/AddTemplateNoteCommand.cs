using BeatKeeper.Exceptions;
using BeatKeeper.Services;
using BeatKeeper.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BeatKeeper.Commands
{
    public class AddTemplateNoteCommand : CommandBase
    {
        private readonly INavigationService<AddTemplateNoteViewModel> _modalNavigationService;

        public AddTemplateNoteCommand(INavigationService<AddTemplateNoteViewModel> modalNavigationService)
        {
            _modalNavigationService = modalNavigationService;
        }

        public override void Execute(object parameter)
        {
            _modalNavigationService.Navigate();
        }
    }
}
