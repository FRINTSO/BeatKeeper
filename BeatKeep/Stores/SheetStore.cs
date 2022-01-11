using BeatKeeper.Models;

namespace BeatKeeper.Stores
{
    public class SheetStore
    {
        public Sheet CurrentSheet { get; set; }
        public Sheet SavedSheet { get; set; }
    }
}
