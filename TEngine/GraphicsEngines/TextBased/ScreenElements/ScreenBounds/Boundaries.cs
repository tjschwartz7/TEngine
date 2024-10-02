using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.GraphicsEngines.TextBased.ScreenElements.ScreenBounds
{
    internal class Boundaries
    {
        private List<Bound> _boundaries;

        public Boundaries()
        {
            _boundaries = new List<Bound>();
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
            _boundaries.Add(new Bound(topLeftRow, topLeftCol, bottomRightRow, bottomRightCol));
            //This will always be the index of the most recently inserted boundary
            return _boundaries.Count() - 1;
        }

        public Bound GetBound(int index)
        {
            return _boundaries[index];
        }

        public int Count()
        {
            return _boundaries.Count();
        }

        public int GetNumRows(int index)
        {
            return _boundaries[index].GetNumRows();
        }

        public int GetNumCols(int index)
        {
            return _boundaries[index].GetNumCols();
        }
    }
}
