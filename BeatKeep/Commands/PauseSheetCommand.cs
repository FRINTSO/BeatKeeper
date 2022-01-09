using BeatKeeper.Stores;
using BeatKeeper.ViewModels;

namespace BeatKeeper.Commands
{
    public class PauseSheetCommand : CommandBase
    {
        private readonly SheetEditorViewModel _sheetEditorViewModel;
        private readonly PlaybackCancellationStore _playbackCancellationStore;

        public PauseSheetCommand(SheetEditorViewModel sheetEditorViewModel, PlaybackCancellationStore playbackCancellationStore)
        {
            _sheetEditorViewModel = sheetEditorViewModel;
            _playbackCancellationStore = playbackCancellationStore;

            _sheetEditorViewModel.IsPlayingChanged += OnCanExecuteChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return _sheetEditorViewModel.IsPlaying && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            _playbackCancellationStore.CurrentCancellationTokenSource.Cancel();
        }

        public void Dispose()
        {
            _sheetEditorViewModel.IsPlayingChanged -= OnCanExecuteChanged;
        }
    }
}
