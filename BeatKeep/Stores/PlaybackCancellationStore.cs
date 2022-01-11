using System.Threading;

namespace BeatKeeper.Stores
{
    public class PlaybackCancellationStore
    {
        private CancellationTokenSource _currentCancellationTokenSource;

        public PlaybackCancellationStore()
        {
            _currentCancellationTokenSource = new();
        }

        public CancellationTokenSource CurrentCancellationTokenSource
        {
            get => _currentCancellationTokenSource;
            set
            {
                _currentCancellationTokenSource.Dispose();
                _currentCancellationTokenSource = value;
            }
        }
    }
}
