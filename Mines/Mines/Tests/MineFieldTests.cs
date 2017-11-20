using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Mines.Tests
{
    [TestFixture]
    public class MineFieldTests
    {
        [Test]
        public void CreateMineField_WithStringAndAppropriateRowCount_IsCreated()
        {
            var testField = new MineField(3, "..*....*.");

            Assert.That(testField, Is.Not.Null);
        }

        [Test]
        public void CreateMineField_WithStringAndInappropriateRowCount_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new MineField(2, "*.."));
        }

        [Test]
        public void CreateMineField_WithListOfStringsAllEqualLength_IsCreated()
        {
            var testField = new MineField(new List<string> { "..*", "...", ".*." });

            Assert.That(testField, Is.Not.Null);
        }

        [Test]
        public void CreateMineField_WithListOfStringsOneShort_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new MineField(new List<string> { "..*", "..", ".*." }));
        }

        [Test]
        public void MineField_SimpleLayout_GeneratesCorrectHintString()
        {
            var expectedHint = "01*1221*1";

            var actualHint = new MineField(3, "..*....*.").GenerateHintString();

            Assert.AreEqual(expectedHint, actualHint);
        }
    }
}
