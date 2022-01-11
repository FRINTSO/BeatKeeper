﻿using BeatKeeper.Commands;
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
        public Sheet Sheet { get; }

        // Notes that are used to store notes added until the sheet is saved, then they will be stored in the real sheet

        private readonly PlaybackCancellationStore _playbackCancellationStore;

        public SheetNoteViewModel SheetNoteViewModel { get; }
        public TemplateNoteListingViewModel TemplateNoteListingViewModel { get; }

        public string Name
        {
            get => Sheet.Name;
            set
            {
                Sheet.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public short BeatsPerMinute
        {
            get => Sheet.BeatsPerMinute;
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

                Sheet.BeatsPerMinute = value;
                OnPropertyChanged(nameof(BeatsPerMinute));
            }
        }

        public event Action IsPlayingChanged;

        private bool _isPlaying;
        public bool IsPlaying
        {
            get => _isPlaying;
            set
            {
                _isPlaying = value;
                IsPlayingChanged?.Invoke();
            }
        }

        public ICommand PlaySheet { get; }
        public ICommand PauseSheet { get; }
        public ICommand SaveSheet { get; }
        public ICommand CloseSheet { get; }

        public SheetEditorViewModel(SheetStore sheetStore, TemplateNotesStore templateNotesStore, MusicBook musicBook, INavigationService<SheetListingViewModel> navigationService)
        {
            Sheet = new(
                sheetStore.CurrentSheet.Name,
                sheetStore.CurrentSheet.BeatsPerMinute,
                sheetStore.CurrentSheet.GetAllNotes().ToList());

            _playbackCancellationStore = new();

            SheetNoteViewModel = new(Sheet, templateNotesStore);
            TemplateNoteListingViewModel = new(Sheet, templateNotesStore);

            PlaySheet = new PlaySheetCommand(this, sheetStore, _playbackCancellationStore);
            PauseSheet = new PauseSheetCommand(this, _playbackCancellationStore);
            SaveSheet = new SaveSheetCommand(musicBook, sheetStore, Sheet);
            CloseSheet = new NavigateCommand<SheetListingViewModel>(navigationService);
        }

        public override void Dispose()
        {
            SheetNoteViewModel.Dispose();
            ((PauseSheetCommand)PauseSheet).Dispose();

            base.Dispose();
        }
    }
}