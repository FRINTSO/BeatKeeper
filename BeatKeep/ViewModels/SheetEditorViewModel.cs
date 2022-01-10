using BeatKeeper.Commands;
using BeatKeeper.Models;
using BeatKeeper.Services;
using BeatKeeper.Stores;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Input;

namespace BeatKeeper.ViewModels
{
    public class SheetEditorViewModel : ViewModelBase
    {
        private readonly SheetStore _sheetStore;
        private readonly PlaybackCancellationStore _playbackCancellationStore;

        public SheetNoteViewModel SheetNoteViewModel { get; }
        public TemplateNoteListingViewModel TemplateNoteListingViewModel { get; }

        public string Name
        {
            get => _sheetStore.CurrentSheet.Name;
            set
            {
                _sheetStore.CurrentSheet.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public short BeatsPerMinute
        {
            get => _sheetStore.CurrentSheet.BeatsPerMinute;
            set
            {

                if (value < 40)
                {
                    value = 40;
                }
                else if (value > 240)
                {
                    value = 240;
                }

                _sheetStore.CurrentSheet.BeatsPerMinute = value;
                OnPropertyChanged(nameof(BeatsPerMinute));
            }
        }

        public ICommand PlaySheet { get; }
        public ICommand PauseSheet { get; }
        public ICommand SaveSheet { get; }
        public ICommand CloseSheet { get; }

        public SheetEditorViewModel(SheetStore sheetStore, TemplateNotesStore templateNotesStore, MusicBook musicBook, IAudioPlayer audioPlayer, INavigationService<SheetListingViewModel> navigationService)
        {
            _sheetStore = sheetStore;
            _playbackCancellationStore = new();
            SheetNoteViewModel = new(sheetStore, templateNotesStore);
            TemplateNoteListingViewModel = new(templateNotesStore);

            PlaySheet = new PlaySheetCommand(audioPlayer);
            PauseSheet = new StopSheetCommand(audioPlayer);
            SaveSheet = new SaveSheetCommand(this, musicBook, sheetStore);
            CloseSheet = new CloseSheetEditorCommand(audioPlayer, navigationService);
        }

        public override void Dispose()
        {
            SheetNoteViewModel.Dispose();
            ((StopSheetCommand)PauseSheet).Dispose();

            base.Dispose();
        }
    }
}
