#include "iostream"
#include <thread>

#define MAX_THREADS 4

class Screen
{
public:
    Screen() {}
    /// @brief All the initialization work for the class members
    /// @param width The width of the screen
    /// @param height The height of the screen
    

    /// @brief Cancel all active threads and stop the screen from running
    static void Stop()
    {
        _stop = true;
    }

    /// @brief This shouldn't be used in most cases
    static void Go()
    {
        _stop = false;
    }

private:
    static char* _screen;
    static bool* _screenHasChanged;
    static int _width;
    static int _height;
    static bool _stop;
    static int _latency;

    struct ScreenBounds
    {
        int rowStart;
        int rowStop;
    };

};