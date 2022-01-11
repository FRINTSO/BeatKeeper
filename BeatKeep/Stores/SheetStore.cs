using BeatKeeper.Models;
using System.Linq;

namespace BeatKeeper.Stores
{
    public class SheetStore
    {
        private Sheet _currentSheet;
        private Sheet _savedSheet;
        public Sheet CurrentSheet
        {
            get => _currentSheet;
            set
            {
                Sheet newSheet = new(value.Name, value.BeatsPerMinute, value.GetAllNotes().ToList());
                _savedSheet = value;
                _currentSheet = newSheet;
            }
        }
        public Sheet SavedSheet
        {
            get => _savedSheet;
        }
    }
}
