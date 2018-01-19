using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShortestEdition;
using System;
using System.Linq;

namespace ShortestEditionTests
{
    [TestClass]
    public class BookTests
    {
        private Book sut;
        private string contents;
        private string[] compressed;

        [TestMethod]
        public void EmptyLinesAreSkipped()
        {
            contents = " " + Environment.NewLine + "row 1" + Environment.NewLine + Environment.NewLine;

            sut = new Book(contents);
            compressed = sut.Compress();

            Assert.AreEqual("row 1", compressed.Single());
        }

        [TestMethod]
        public void ShortLinesAreConcatenated()
        {
            contents = "row1" + Environment.NewLine + "row2";

            sut = new Book(contents);
            compressed = sut.Compress();

            Assert.AreEqual("row1 row2", compressed.Single());
        }

        [TestMethod]
        public void WhiteSpacesInTheBeginningAndEndOfRowAreIgnored()
        {
            contents = "    row1.   " + Environment.NewLine + "  Row2, ";

            sut = new Book(contents);
            compressed = sut.Compress();

            Assert.AreEqual("row1. Row2,", compressed.Single());
        }
    }
}
