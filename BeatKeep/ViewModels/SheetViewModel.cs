using BeatKeeper.Commands;
using BeatKeeper.Models;
using BeatKeeper.Services.SheetEditorLoader;
using System;
using System.Windows.Input;

namespace BeatKeeper.ViewModels
{
    public class SheetViewModel : ViewModelBase
    {
        private readonly Sheet _sheet;

        public SheetViewModel(Sheet sheet, MusicBook musicBook, ISheetEditorLoader sheetEditorLoader)
        {
            _sheet = sheet;
            OpenSheet = new OpenSheetCommand(this, musicBook, sheetEditorLoader);
            RemoveSheet = new DeleteSheetCommand(this, musicBook);
        }

        public Guid Id => _sheet.Id;
        public string Name { get => _sheet.Name; set => _sheet.Name = value; }
        public short BeatsPerMinute { get => _sheet.BeatsPerMinute; set => _sheet.BeatsPerMinute = value; }
        public string Length => _sheet.Length.ToString(@"m\:ss");
        public ICommand OpenSheet { get; }
        public ICommand RemoveSheet { get; }

    }
}
