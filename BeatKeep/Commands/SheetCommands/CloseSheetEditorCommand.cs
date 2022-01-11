using BeatKeeper.Services;
using BeatKeeper.ViewModels;

namespace BeatKeeper.Commands
{
    /// <summary>
    /// Class command for closing the sheet editor and navigating to sheet listing.
    /// </summary>
    public class CloseSheetEditorCommand : CommandBase
    {
        private readonly IAudioPlayer _audioPlayer;
        private readonly INavigationService<SheetListingViewModel> _navigationService;

        public CloseSheetEditorCommand(IAudioPlayer audioPlayer, INavigationService<SheetListingViewModel> navigationService)
        {
            _audioPlayer = audioPlayer;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            if (_audioPlayer.IsPlaying)
            {
                _audioPlayer.Stop();
            }
            _navigationService.Navigate();
        }
    }
}
