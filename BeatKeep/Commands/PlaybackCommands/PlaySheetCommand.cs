using BeatKeeper.Services;
using System.Threading.Tasks;

namespace BeatKeeper.Commands
{
    /// <summary>
    /// Class command for playing audio.
    /// </summary>
    public class PlaySheetCommand : AsyncCommandBase
    {
        private readonly IAudioPlayer _audioPlayer;

        public PlaySheetCommand(IAudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
        }

        public override Task ExecuteAsync(object parameter)
        {
            return _audioPlayer.PlayAsync();
        }
    }
}
