using BeatKeeper.Models;
using BeatKeeper.Stores;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BeatKeeper.Services
{
    public class AudioPlayer : IAudioPlayer
    {
        private const int SOUND_PLAY_DURATION = 490;

        private readonly SheetStore _sheetStore;
        private readonly WaveFileReader _waveReader;
        private readonly WaveChannel32 _channel;

        private CancellationTokenSource _cancellationTokenSource;
        private bool _isPlaying;

        public AudioPlayer(SheetStore sheetStore)
        {
            _sheetStore = sheetStore;

            _waveReader = new(Path.GetFullPath(@".\Track1_1.wav"));
            _channel = new WaveChannel32(_waveReader);
        }

        public event Action IsPlayingChanged;

        public bool IsPlaying
        {
            get => _isPlaying;
            set
            {
                _isPlaying = value;
                IsPlayingChanged?.Invoke();
            }
        }

        public async Task PlayAsync()
        {
            _cancellationTokenSource = new();

            IsPlaying = true;

            List<int> noteLengths = _sheetStore.CurrentSheet.GetAllNotes()
                .Select(
                x => (int)(x.GetLength(_sheetStore.CurrentSheet.BeatsPerMinute) * 1000d))
                .ToList();

            foreach (int noteLength in noteLengths)
            {
                Thread t = new(new ThreadStart(PlaySound));
                t.Start();

                try
                {
                    await Task.Delay(noteLength, _cancellationTokenSource.Token);
                }
                catch (TaskCanceledException)
                {
                    _cancellationTokenSource.Dispose();
                    break;
                }
            }

            IsPlaying = false;
        }

        public void Stop()
        {
            if (IsPlaying)
            {
                _cancellationTokenSource.Cancel();
                IsPlaying = false;
            }
        }

        private void PlaySound()
        {
            DirectSoundOut _output = new();
            _channel.CurrentTime = _channel.TotalTime.Subtract(TimeSpan.FromMilliseconds(SOUND_PLAY_DURATION));

            _output.Init(_channel);
            _output.Play();
        }
    }
}
