using BeatKeeper.Commands;
using BeatKeeper.Models;
using BeatKeeper.Services;
using BeatKeeper.Stores;
using System;
using System.Windows.Input;

namespace BeatKeeper.ViewModels
{
    public class SheetViewModel : ViewModelBase
    {
        private readonly Sheet _sheet;

        public Guid Id => _sheet.Id;
        public string Name { get => _sheet.Name; set => _sheet.Name = value; }
        public short BeatsPerMinute { get => _sheet.BeatsPerMinute; set => _sheet.BeatsPerMinute = value; }
        public string Length => _sheet.Length.ToString(@"m\:ss");

        public ICommand OpenSheet { get; }
        public ICommand RemoveSheet { get; }

        public SheetViewModel(Sheet sheet, SheetStore sheetStore, MusicBook musicBook, INavigationService<SheetEditorViewModel> navigationService)
        {
            _sheet = sheet;
            OpenSheet = new OpenSheetCommand(this, sheetStore, musicBook, navigationService);
            RemoveSheet = new RemoveSheetCommand(this, musicBook);
        }
    }
}
