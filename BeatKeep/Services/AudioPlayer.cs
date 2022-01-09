using System.Media;

namespace BeatKeeper.Services
{
    public class AudioPlayer : IAudioPlayer
    {
        private readonly SoundPlayer _player = new(@"C:\Users\willi\source\repos\BeatKeep\BeatKeep\New Project - Instrument (1).wav");

        public AudioPlayer()
        {
            Load();
        }

        public void Play()
        {
            _player.PlayLooping();
        }

        public void Load()
        {
            if (!_player.IsLoadCompleted)
            {
                _player.LoadAsync();
            }
        }

        public void Dispose()
        {
            _player.Dispose();
        }
    }
}
