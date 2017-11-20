namespace Mines
{
    /// <summary>
    /// Representation of a cell in a minesweeper grid
    /// 
    /// Records the X and Y position of the cell, whether it is a mine, and
    /// if a given cell is a neighbour of it
    /// </summary>
    public class Cell
    {
        public bool IsMine { get; set; }

        public int X { get; protected set; }
        public int Y { get; protected set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool IsNeighborOf(Cell cell)
        {
            return IsNeighborOf(cell.X, cell.Y);
        }

        public bool IsNeighborOf(int i, int j)
        {
            return X >= i - 1 && X <= i + 1
                && Y >= j - 1 && Y <= j + 1;
        }
    }
}
