using BeatKeeper.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatKeeper.Commands
{
    public class BrowseImagesCommand : CommandBase
    {
        private readonly AddTemplateNoteViewModel _addTemplateNoteViewModel;

        public BrowseImagesCommand(AddTemplateNoteViewModel addTemplateNoteViewModel)
        {
            _addTemplateNoteViewModel = addTemplateNoteViewModel;
        }

        public override void Execute(object parameter)
        {
            using OpenFileDialog dialog = new();

            dialog.Filter = "Png Images|*.png";
            
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _addTemplateNoteViewModel.TemplateNoteImageSource = dialog.FileName;
            }
        }
    }
}
