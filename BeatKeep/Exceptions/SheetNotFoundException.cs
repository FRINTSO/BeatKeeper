using BeatKeeper.Models;
using System;

namespace BeatKeeper.Exceptions
{
    public class SheetNotFoundException : Exception
    {
        public Sheet Sheet { get; }

        public SheetNotFoundException(Sheet sheet = null)
        {
            Sheet = sheet;
        }

        public SheetNotFoundException(string message, Sheet sheet = null) : base(message)
        {
            Sheet = sheet;
        }

        public SheetNotFoundException(string message, Exception innerException, Sheet sheet = null) : base(message, innerException)
        {
            Sheet = sheet;
        }
    }
}
