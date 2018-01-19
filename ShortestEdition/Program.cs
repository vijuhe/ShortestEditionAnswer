using System;
using System.IO;

namespace ShortestEdition
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Arguments: input file, output file");
                Console.ReadLine();
            }
            var started = DateTime.Now;
            string bookFile = args[0];
            string compressedBookFile = args[1];
            var book = new Book(File.ReadAllText(bookFile));
            string[] compressedContent = book.Compress();
            File.WriteAllLines(compressedBookFile, compressedContent);
            var ended = DateTime.Now;
            Console.WriteLine($"Book in '{bookFile}' compressed to {compressedBookFile}.");
            Console.WriteLine($"Execution time {ended.Subtract(started).TotalSeconds} seconds.");
            Console.ReadLine();
        }
    }
}
