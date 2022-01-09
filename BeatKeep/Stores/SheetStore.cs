using BeatKeeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatKeeper.Stores
{
    public class SheetStore
    {
        private Sheet _currentSheet;
        public Sheet CurrentSheet
        {
            get => _currentSheet;
            set
            {
                _currentSheet = value;
            }
        }
    }
}
