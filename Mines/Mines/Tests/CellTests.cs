using NUnit.Framework;

namespace Mines.Tests
{
    [TestFixture]
    class CellTests
    {
        [Test]
        public void IsNeighbor_CoordinateWithinRange_ReturnsTrue()
        {
            var testCell = new Cell(0, 0);

            Assert.That(testCell.IsNeighborOf(1, 1), Is.True);
        }

        [Test]
        public void IsNeighbor_CoordinateOutsideRange_ReturnsFalse()
        {
            var testCell = new Cell(0, 0);

            Assert.That(testCell.IsNeighborOf(2, 1), Is.False);
        }

        [Test]
        public void IsNeighbor_CellWithinRange_ReturnsTrue()
        {
            var testCell = new Cell(0, 0);

            Assert.That(testCell.IsNeighborOf(new Cell(1, 1)), Is.True);
        }

        [Test]
        public void IsNeighbor_CellOutsideRange_ReturnsFalse()
        {
            var testCell = new Cell(0, 0);

            Assert.That(testCell.IsNeighborOf(new Cell(1, 2)), Is.False);
        }
    }
}
