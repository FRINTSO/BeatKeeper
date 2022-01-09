using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatKeeper.Services
{
    public interface IAudioPlayer
    {
        void Play();
        void Load();
        void Dispose();
    }
}
