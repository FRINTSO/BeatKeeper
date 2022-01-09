using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BeatKeeper.Services
{
    public class MediaPlayerCustom : IAudioPlayer
    {
        private readonly MediaPlayer _player = new();

        public MediaPlayerCustom()
        {
            Uri uri = new(@"C:\Users\willi\source\repos\BeatKeep\BeatKeep\myBeepFile.mp3");
            _player.Open(uri);
        }

        public void Play()
        {
            _player.Play();
        }

        public void Load()
        {
            _player.Position = TimeSpan.Zero;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
