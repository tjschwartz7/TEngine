extern "C"             //No name mangling
__declspec(dllexport)  //Tells the compiler to export the function
int                    //Function return type     
__cdecl                //Specifies calling convention, cdelc is default, 
                       //so this can be omitted 




/// @brief Sits there and watches the screen do its thing. Blocking function.
void ScreenWatchdog()
{
    int slice = _height / MAX_THREADS;

    int numThreads = MAX_THREADS;
    //Assuming one thread can easily handle 400 rows
    int maxNeededThreads = (_height / 400); //I.E. 5 threads for 2000 rows, 4 for 1600
    if (numThreads > maxNeededThreads) numThreads = maxNeededThreads;

    std::thread* threads = new std::thread[numThreads];

    if (numThreads == 1)
    {
        ScreenBounds bound;
        bound.rowStart = 0;
        bound.rowStop = _height - 1; //Top row
        threads[0] = std::thread(Print, bound);
    }
    else
    {
        int slice = _height / numThreads;
        for (int i = _height; i > 0; i -= slice)
        {
            ScreenBounds bound;
            bound.rowStart = i;

            //If this is the last iteration of the loop
            if (i - slice <= 0)
            {
                //The final bound is just 0
                bound.rowStop = 0;
            }
            else
            {
                //It's one above i - slice
                bound.rowStop = i - slice + 1;
            }

            // Create a thread and pass a function and its arguments
            threads[i] = std::thread(Print, bound);
        }
    }

    //Wait for each thread sequentially (they'll all terminate at once!)
    for (int i = 0; i < numThreads; i++)
    {
        //Join the thread
        threads[i].join();
    }
}

/// @brief Print out the screen.
/// @param bounds A struct containing the boundaries of the screen.
void Print(void* bounds)
{
    int startIndex = 0;
    int stopIndex = 0;

    //Make a new scope to prevent excess stack usage in thread
    //We don't care how long this takes: it only runs once.
    {
        struct ScreenBounds bound = *(struct ScreenBounds*)bounds;
        int rowStart = bound.rowStart;
        int rowStop = bound.rowStop;
        //Perform as many calculations before the loop as possible
        startIndex = rowStart * _width;
        stopIndex = rowStop * _width;
    }

    //This thread will end when Stop() is called
    while (!_stop)
    {
        for (int i = startIndex; i < stopIndex; i++)
        {

            int row = i / _height;
            int col = i % _width;
            //$"\033[{i};0H{_screen[row, col]}"
            //Replace character at this index
            //But only if its changed
            if (_screenHasChanged[i])
                printf("\033[%d;%dH%c", row, col, _screen[i]);
        }
        std::this_thread::sleep_for(std::chrono::milliseconds(_latency));
    }
}


/// @brief Updates the screen in a rectangular fashion from the top left coordinate to the bottom right coordinate.
/// @param charArray The character array to write over the screen. Must be the same size as your rectangular selection. Or else there will be garbage output. I don't know what size this thing is.
/// @param topLeftRow The row of the top left coordinate.
/// @param topLeftCol The col of the top left coordinate.
/// @param bottomRightRow The row of the bottom right coordinate.
/// @param bottomRightCol The col of the bottom right coordinate.
void UpdateScreen(char* charArray, int topLeftRow, int topLeftCol, int bottomRightRow, int bottomRightCol)
{
    //Invalid
    if (!charArray) return;
    int startIndex = topLeftRow * _height + topLeftCol;
    int stopIndex = bottomRightRow * _height + bottomRightCol;

    //Iterate over each character in the selection
    for (int i = startIndex; i <= stopIndex; i++)
    {
        if (charArray[i]) //Check if this is not null (won't fix it if its garbage data...)  
        {
            //Check if the pixels are different
            if (_screen[i] != charArray[i]) _screenHasChanged[i] = true;
            _screen[i] = charArray[i];
        }
    }
}

/// @brief Updates a specific pixel on the screen.
/// @param pixel The new pixel to overwrite the previous one.
/// @param row The row of the new pixel.
/// @param col The column of the new pixel.
void UpdatePixel(char pixel, int row, int col)
{
    int index = row * _height + col;
    if (_screen[index] != pixel) _screenHasChanged[index] = true;
    _screen[index] = pixel;
}