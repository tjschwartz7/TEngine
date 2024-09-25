// Screen.cpp (C++/CLI)
#include "Screen.cpp"  // Import your native C++ code

public ref class MathWrapper {
public:
    // This is a managed method that C# can call
    void SetupScreen(int width, int height) 
    {
        _height = height;
        _width = width;
        Initialize(width, height);  // Calls the native C++ function
    }

    void UpdateSinglePixel(char pixel, int row, int col);
    {
        UpdatePixel(pixel, row, col)
    }

    void UpdateScreenSelection(string strSelection, int topLeftRow, int topLeftCol, int bottomRightRow, int bottomRightCol)
    {
        //The bottom row is larger than the top row
        //Subtract actual indices to get the size in chars
        int numPixelsInFinal = (bottomRightRow*_height + bottomRightCol) - (topLeftRow*_height + topLeftCol);

        if(charArray.size() != numPixelsInFinal)
        {
            std::cerr << "SCREENWRAPPER.CPP >> Wrong number of characters in charArray!! Must equal the total number of pixels in your selection.\n";
            abort();
        }

        char charArray[numPixelsInFinal];
        strncpy(charArray, strSelection.c_str(), numPixelsInFinal);
        UpdateScreen(charArray, topLeftRow, topLeftCol, bottomRightRow, bottomRightCol);
    }
private:
    int _height;
    int _width;
};