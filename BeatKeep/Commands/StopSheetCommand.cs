using BeatKeeper.Services;

namespace BeatKeeper.Commands
{
    public class StopSheetCommand : CommandBase
    {
        private readonly IAudioPlayer _audioPlayer;

        public StopSheetCommand(IAudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
            _audioPlayer.IsPlayingChanged += OnCanExecuteChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return _audioPlayer.IsPlaying && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            _audioPlayer.Stop();
        }

        public void Dispose()
        {
            _audioPlayer.IsPlayingChanged -= OnCanExecuteChanged;
        }

    }
}
