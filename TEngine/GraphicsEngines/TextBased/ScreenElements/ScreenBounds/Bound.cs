using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.GraphicsEngines.TextBased.ScreenElements.ScreenBounds
{
    internal class Bound
    {
        Tuple<int, int> _topLeftCoords;
        Tuple<int, int> _bottomRightCoords;
        int _numRows;
        int _numCols;
        int _numPixels;
        public Bound(int topLeftRow, int topLeftCol, int bottomRightRow, int bottomRightCol)
        {
            _topLeftCoords = new Tuple<int, int>(topLeftRow, topLeftCol);
            _bottomRightCoords = new Tuple<int, int>(bottomRightRow, bottomRightCol);
            _numCols = bottomRightCol - topLeftCol + 1; //High - low + 1
            _numRows = bottomRightRow - topLeftRow + 1; //High - low + 1
            _numPixels = _numRows * _numCols; //Just the area of the surface
        }

        /// <summary>
        ///Get the top left row coordinate.
        /// </summary>
        /// <returns>The row coordinate.</returns>
        public int GetTopLeftRow() { return _topLeftCoords.Item1; }
        /// <summary>
        /// Get the top left column coordinate.
        /// </summary>
        /// <returns>The column coordinate.</returns>
        public int GetTopLeftCol() { return  _topLeftCoords.Item2; }
        /// <summary>
        /// Get the bottom right row coordinate.
        /// </summary>
        /// <returns>The row coordinate.</returns>
        public int GetBottomRightRow() { return _bottomRightCoords.Item1; }
        /// <summary>
        /// Get the bottom right column coordinate.
        /// </summary>
        /// <returns>The column coordinate.</returns>
        public int GetBottomRightCol() { return _bottomRightCoords.Item2; }

        /// <summary>
        /// Get the number of columns in the surface bound.
        /// </summary>
        public int GetNumCols() { return _numCols; }    
        /// <summary>
        /// Get the number of rows in the surface bound
        /// </summary>
        public int GetNumRows() { return _numRows; }

        /// <summary>
        /// Get the number of pixels in the entire surface.
        /// This is essentially just the area of the surface.
        /// </summary>
        public int GetNumPixels() { return _numPixels; }
    }
}
