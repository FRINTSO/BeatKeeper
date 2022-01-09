using System;

namespace BeatKeeper.Models
{
    public class Note
    {
        public Guid Id { get; set; }
        public double RelativeDuration { get; }
        public byte Dots { get; }
        public double Duration => RelativeDuration + (RelativeDuration * (Math.Pow(2, Dots) - 1) / Math.Pow(2, Dots));

        public Note(double relativeDuration, byte dots)
        {
            Id = Guid.NewGuid();
            RelativeDuration = relativeDuration;
            Dots = dots;
        }

        /// <summary>
        /// Get the note length in seconds.
        /// </summary>
        /// <param name="beatsPerMinute"></param>
        /// <returns>The note length in seconds.</returns>
        public double GetLength(short beatsPerMinute)
        {
            return 240d / beatsPerMinute * Duration;
        }
    }
}
