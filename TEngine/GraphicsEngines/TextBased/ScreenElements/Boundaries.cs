using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.GraphicsEngines.TextBased.ScreenElements
{
    internal class Boundaries
    {
        private List<Tuple<int, int, int, int>> _boundaries;
        private List<Tuple<int, int>> _numRowsCols;

        public Boundaries()
        {
            _boundaries = new List<Tuple<int, int, int, int>>();
        }

        /// <summary>
        /// Adds a new boundary and returns its index. 
        /// </summary>
        /// <param name="topLeftRow">The top left row of our selection.</param>
        /// <param name="topLeftCol">The top left column of our selection.</param>
        /// <param name="bottomRightRow">The bottom right row of our selection.</param>
        /// <param name="bottomRightCol">The bottom right column of our selection.</param>
        /// <returns>
        /// The index of the newly created boundary. 
        /// It's recommended that you keep track of these indices for your own usage, and perhaps make an enum to map the number to what it is.
        /// </returns>
        public int AddBoundary(int topLeftRow, int topLeftCol, int bottomRightRow, int bottomRightCol)
        {
            _boundaries.Add(new Tuple<int, int, int, int>(topLeftRow, topLeftCol, bottomRightRow, bottomRightCol));
            Tuple<int, int> numRowCol;
            int numCols = bottomRightCol - topLeftCol;
            int numRows = bottomRightRow - topLeftRow;
            numRowCol = new Tuple<int, int>(numRows, numCols);
            _numRowsCols.Add(numRowCol);
            //This will always be the index of the most recently inserted boundary
            return _boundaries.Count() - 1;
        }

        public Tuple<int,int,int,int> GetBound(int index)
        {
            return _boundaries[index];
        }

        public int Count()
        {
            return _boundaries.Count();
        }

        public int getNumRows(int index)
        {
            return _numRowsCols[index].Item1;
        }

        public int getNumCols(int index) 
        {
            return _numRowsCols[index].Item2;
        }
    }
}
