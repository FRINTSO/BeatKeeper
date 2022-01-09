using BeatKeeper.Models;
using System;

namespace BeatKeeper.Exceptions
{
    public class SheetConflictException : Exception
    {
        public Sheet ExistingSheet { get; }
        public Sheet IncomingSheet { get; }

        public SheetConflictException(Sheet existingSheet, Sheet incomingSheet)
        {
            ExistingSheet = existingSheet;
            IncomingSheet = incomingSheet;
        }

        public SheetConflictException(string message, Sheet existingSheet, Sheet incomingSheet) : base(message)
        {
            ExistingSheet = existingSheet;
            IncomingSheet = incomingSheet;
        }

        public SheetConflictException(string message, Exception innerException, Sheet existingSheet, Sheet incomingSheet) : base(message, innerException)
        {
            ExistingSheet = existingSheet;
            IncomingSheet = incomingSheet;
        }
    }
}
