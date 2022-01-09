using System;
using System.Collections.Generic;

namespace BeatKeeper.Models
{
    public class Sheet
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public short BeatsPerMinute { get; set; }
        private readonly List<Note> _notes;

        public TimeSpan Length => CalculateSongLength();

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

        public IEnumerable<Note> GetAllNotes()
        {
            return _notes;
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

        public void RemoveNoteById(Guid id)
        {
            int index = _notes.FindIndex(sheet => sheet.Id == id);

            if (index != -1)
            {
                _notes.RemoveAt(index);
                return;
            }

            throw new Exception("Note not found.");
        }
    }
}
