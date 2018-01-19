using System;
using System.Collections.Generic;
using System.Linq;

namespace ShortestEdition
{
    internal class Row
    {
        private const int RowMaxLength = 80;

        public Row()
        {
            Content = string.Empty;
        }

        public string Content { get; private set; }
        public bool IsFullOrUnfillable => RowMaxLength - Content.Length < 2;

        public void AddWord(string word)
        {
            if (Content.Equals(string.Empty))
            {
                Content = word;
            }
            else
            {
                Content = Content + " " + word;
            }
        }

        public bool IsFullOrCanBeFilledAfterAdding(string word, int minSpaceToLeave)
        {
            return IsFullAfterAdding(word) || CanBeFilledAfterAdding(word, minSpaceToLeave);
        }

        public bool IsFullAfterAdding(string word)
        {
            return LengthAfterAdding(word) == RowMaxLength;
        }

        public bool CanBeFilledAfterAdding(string word, int minSpaceToLeave)
        {
            return LengthAfterAdding(word) < RowMaxLength - minSpaceToLeave;
        }

        public bool CanAdd(string word)
        {
            return LengthAfterAdding(word) <= RowMaxLength;
        }

        public string RemoveWordOfLength(int length)
        {
            List<string> words = Content.Split(' ').ToList();
            string removedWord = words.FirstOrDefault(w => w.Length == length);
            if (removedWord != null)
            {
                words.Remove(removedWord);
                Content = string.Join(' ', words);
            }
            return removedWord;
        }

        private int LengthAfterAdding(string word)
        {
            return Content.Length + word.Length + 1;
        }
    }
}