using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mines
{
    /// <summary>
    /// Representation of a minesweeper grid.
    /// 
    /// May be constructed from a string representation of the grid, with a specified
    /// number of columns, or from an array of strings, each representing one row of
    /// the grid.
    /// 
    /// Contains methods to return the hint grid (i.e. how many mines surround each cell)
    /// as either a single string, or an enumerable of strings.
    /// </summary>
    class MineField
    {
        private IList<Cell> _cells;

        private int _rows;
        private int _cols;

        /// <summary>
        /// Constructs a minefield from a single string, representint the cells. Requires the number of rows in the
        /// grid to also be supplied.
        /// 
        /// If the length of the supplied layout string is not an integer multiple number of rows, an <see cref="ArgumentException"/>
        /// will be thrown.
        /// </summary>
        /// <param name="rows">Number of rows in the grid</param>
        /// <param name="layout">Layout of the grid, one row after another</param>
        public MineField(int rows, string layout)
        {
            if(layout.Length % rows != 0)
            {
                throw new ArgumentException("Incompatible layout and row count");
            }

            _rows = rows;
            _cols = layout.Length / rows;

            _cells = new List<Cell>();

            foreach(var cell in layout.Select((c, i) => new { idx = i, isMine = c == '*' }))
            {
                var i = cell.idx % _cols;
                var j = cell.idx / _cols;

                _cells.Add(new Cell(i, j)
                {
                    IsMine = cell.isMine
                });
            }
        }

        /// <summary>
        /// Constructs a mine field from an enumerable of strings, where each element in the enumerable
        /// represents a row.
        /// 
        /// If the strings in the enumerable are not all of equal length, an argument exception will
        /// be thrown
        /// </summary>
        /// <param name="layout">Enumerbale of strings representing the mine layout</param>
        public MineField(IEnumerable<string> layout)
            : this(layout.Count(), ProcessEnumerableLayout(layout))
        { }

        /// <summary>
        /// Pre-processor for constructing the minefield from an enumerable of strings.
        /// Ensures that all elements are the same length, then flattens to a single string.
        /// </summary>
        /// <param name="layout"></param>
        /// <returns></returns>
        private static string ProcessEnumerableLayout(IEnumerable<string> layout)
        {
            if(!layout.All(s => s.Length == layout.First().Length))
            {
                throw new ArgumentException("Not all rows are equal length");
            }

            return layout.Combine();
        }

        /// <summary>
        /// Generates a hint string - the count of mines surrounding a given cell.
        /// </summary>
        /// <returns>A string representing the number of mines around each cell, or if the cell
        /// is a mine itself.</returns>
        public string GenerateHintString()
        {
            var sb = new StringBuilder();

            foreach(var cell in _cells)
            {
                sb.Append(cell.IsMine ? "*" : _cells.Count(c => c.IsNeighborOf(cell) && c.IsMine).ToString("0"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Generates a hint “grid” - the count of mines around each cell -, to be displayed
        /// as a grid representation
        /// </summary>
        /// <returns>Enumerable of strings, each representing a row in the grid, with the number of
        /// mines around that cell, or if the cell is a mine itself</returns>
        public IEnumerable<string> GenerateHintGrid()
        {
            return GenerateHintString().BreakToLength(_cols);
        }
    }
}
