using BeatKeeper.Models;

namespace BeatKeeper.Stores
{
    public class SheetStore
    {
        private Sheet _currentSheet;
        public Sheet CurrentSheet
        {
            get => _currentSheet;
            set => _currentSheet = value;
        }
    }
}
