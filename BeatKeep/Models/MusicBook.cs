using BeatKeeper.Exceptions;
using System;
using System.Collections.Generic;

namespace BeatKeeper.Models
{
    public class MusicBook
    {
        private readonly List<Sheet> _sheets;

        public MusicBook()
        {
            _sheets = new List<Sheet>();
        }

        public IEnumerable<Sheet> GetAllSheets()
        {
            return _sheets;
        }

        public void AddSheet(Sheet sheet)
        {
            foreach (Sheet existingSheet in _sheets)
            {
                if (existingSheet.Id == sheet.Id)
                {
                    throw new SheetConflictException(existingSheet, sheet);
                }
            }

            _sheets.Add(sheet);
        }

        public bool ContainsSheetById(Guid Id)
        {
            return _sheets.Exists(sheet => sheet.Id == Id);
        }

        public Sheet GetSheetById(Guid Id)
        {
            return _sheets.Find(sheet => sheet.Id == Id);
        }

        public void DeleteSheetById(Guid Id)
        {
            int index = _sheets.FindIndex(sheet => sheet.Id == Id);

            if (index != -1)
            {
                _sheets.RemoveAt(index);
                return;
            }

            throw new SheetNotFoundException();
        }
    }
}
