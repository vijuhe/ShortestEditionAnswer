using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ShortestEdition
{
    public class Book
    {
        private string text;

        public Book(string text)
        {
            this.text = text;
        }

        public string[] Compress()
        {
            var result = new List<Row>();
            var words = SplitTextWordsAndOrderByDescendingLength().ToList();
            while (words.Any())
            {
                result.Add(CreateRow(words));
            }
            return result.Select(row => row.Content).ToArray();
        }

        private IEnumerable<string> SplitTextWordsAndOrderByDescendingLength()
        {
            return Regex.Split(text, @"(\s|\r?\n)+")
                .Where(word => !string.IsNullOrWhiteSpace(word))
                .OrderByDescending(word => word.Length);
        }

        private Row CreateRow(List<string> lengthOrderedWords)
        {
            var row = new Row();
            while(!row.IsFullOrUnfillable && lengthOrderedWords.Any())
            {
                int shortestWordLength = lengthOrderedWords.Last().Length;
                string word = lengthOrderedWords.FirstOrDefault(w => 
                    row.IsFullOrCanBeFilledAfterAdding(w, shortestWordLength));
                if (word != null)
                {
                    MoveWordToRow(lengthOrderedWords, row, word);
                    continue;
                }
                word = lengthOrderedWords.FirstOrDefault(w => row.CanAdd(w));
                if (word != null)
                {
                    if (word.Length * 2 == shortestWordLength * 3 &&
                        lengthOrderedWords.Count(w => w.Length == shortestWordLength) > 2)
                    {
                        string removedWord = row.RemoveWordOfLength(word.Length);
                        if (removedWord != null)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                row.AddWord(lengthOrderedWords.Last());
                                lengthOrderedWords.RemoveAt(lengthOrderedWords.Count - 1);
                            }
                            lengthOrderedWords.Insert(0, removedWord);
                            return row;
                        }
                    }
                    MoveWordToRow(lengthOrderedWords, row, word);
                    return row;
                }
                return row;
            }
            return row;
        }

        private static void MoveWordToRow(List<string> words, Row row, string word)
        {
            row.AddWord(word);
            words.Remove(word);
        }
    }
}