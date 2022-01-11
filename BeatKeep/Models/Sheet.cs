using System;
using System.Collections.Generic;

namespace BeatKeeper.Models
{
    public class Sheet
    {
        private readonly List<Note> _notes;

        public Sheet(string name, short beatsPerMinute) : this(name, beatsPerMinute, new List<Note>())
        {
        }

        public Sheet(string name, short beatsPerMinute, List<Note> notes)
        {
            Id = Guid.NewGuid();
            Name = name;
            BeatsPerMinute = beatsPerMinute;
            _notes = notes;
        }

        public Guid Id { get; }
        public string Name { get; set; }
        public short BeatsPerMinute { get; set; }
        public TimeSpan Length => CalculateSongLength();

        public IEnumerable<Note> GetAllNotes()
        {
            return _notes;
        }

        public void MoveNotes(int oldIndex, int newIndex)
        {
            Note removedItem = _notes[oldIndex];

            _notes.RemoveAt(oldIndex);
            _notes.Insert(newIndex, removedItem);
        }

        public void AddNote(Note note)
        {
            _notes.Add(note);
        }

        public void DeleteNoteById(Guid id)
        {
            int index = _notes.FindIndex(sheet => sheet.Id == id);

            if (index != -1)
            {
                _notes.RemoveAt(index);
                return;
            }

            throw new Exception("Note not found.");
        }

        private TimeSpan CalculateSongLength()
        {
            double noteLengths = 0;

            foreach (Note note in _notes)
            {
                noteLengths += note.GetLength(BeatsPerMinute);
            }

            return TimeSpan.FromSeconds(noteLengths);
        }
    }
}
