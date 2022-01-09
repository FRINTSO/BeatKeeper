using BeatKeeper.Stores;
using BeatKeeper.ViewModels;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BeatKeeper.Commands
{
    public class PlaySheetCommand : AsyncCommandBase
    {
        private readonly SheetEditorViewModel _sheetEditorViewModel;
        private readonly SheetStore _sheetStore;
        private readonly PlaybackCancellationStore _playbackCancellationStore;

        private readonly WaveFileReader _waveReader;
        private readonly WaveChannel32 _channel;

        private const int SOUND_PLAY_DURATION = 490;

        public PlaySheetCommand(SheetEditorViewModel sheetEditorViewModel, SheetStore sheetStore, PlaybackCancellationStore playbackCancellationStore)
        {
            _sheetEditorViewModel = sheetEditorViewModel;
            _sheetStore = sheetStore;
            _playbackCancellationStore = playbackCancellationStore;

            _waveReader = new(Path.GetFullPath(@".\Track1_1.wav"));
            _channel = new(_waveReader);
        }

        public override Task ExecuteAsync(object parameter)
        {
            return PlaySoundThreadSleep();
        }

        private async Task PlaySoundThreadSleep()
        {
            _sheetEditorViewModel.IsPlaying = true;

            List<int> noteLengths = _sheetStore.CurrentSheet.GetAllNotes()
                .Select(
                x => (int)(x.GetLength(_sheetStore.CurrentSheet.BeatsPerMinute) * 1000d))
                .ToList();

            foreach (int noteLength in noteLengths)
            {
                Thread t = new(new ThreadStart(PlaySoundWithNAudio));
                t.Start();
                try
                {
                    await Task.Delay(noteLength, _playbackCancellationStore.CurrentCancellationTokenSource.Token);
                }
                catch (TaskCanceledException)
                {
                    _playbackCancellationStore.CurrentCancellationTokenSource = new();
                    break;
                }
            }

            _sheetEditorViewModel.IsPlaying = false;
        }

        private void CreateWavAudioFileWithNAudio()
        {
            throw new NotImplementedException();

            //WaveMixerStream32 mixer = new();
            //List<double> startTimes = _sheetStore.CurrentSheet.GetAllNotes().Select(x => x.StartTimeSeconds).ToList();

            //foreach (double startTime in startTimes)
            //{
            //    WaveFileReader wave = new(Path.GetFullPath(@".\Track1_1.wav"));

            //    WaveOffsetStream waveOffsetStream = new(wave, TimeSpan.FromSeconds(startTime), TimeSpan.Zero, TimeSpan.FromMilliseconds(500));

            //    WaveChannel32 channel = new(waveOffsetStream);

            //    mixer.AddInputStream(channel);
            //}

            //WaveFileWriter.CreateWaveFile("composition.wav", new Wave32To16Stream(mixer));
        }

        private void PlaySoundWithNAudio()
        {
            DirectSoundOut _output = new();
            _channel.CurrentTime = _channel.TotalTime.Subtract(TimeSpan.FromMilliseconds(SOUND_PLAY_DURATION));

            _output.Init(_channel);
            _output.Play();
        }
    }
}
