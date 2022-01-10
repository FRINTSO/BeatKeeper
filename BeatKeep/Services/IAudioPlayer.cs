using System;
using System.Threading.Tasks;

namespace BeatKeeper.Services
{
    public interface IAudioPlayer
    {
        event Action IsPlayingChanged;

        bool IsPlaying { get; }

        Task PlayAsync();
        void Stop();
    }
}
