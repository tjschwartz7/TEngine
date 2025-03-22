using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.GraphicsEngines.TextBased;

namespace TEngine.Helpers
{
    public static class MessageUtils
    {
        /// <summary>
        /// Prints to stderr and terminates the program.
        /// </summary>
        /// <param name="error">The error message.</param>
        public static void TerminateWithError(string errorClass, string errorMethod, string message)
        {
            oldApplication.SetErrorFlag();
            TextWriter errorWriter = Console.Error;
            errorWriter.WriteLine($"{errorClass} -> {errorMethod}: {message}");
            oldApplication.TerminateApplication();
            TextBasedEngine.TerminateWindow();
        }

        /// <summary>
        /// Sets the Application error message and flag. Does not terminate the program.
        /// </summary>
        /// <param name="message">The error message.</param>
        public static void SetErrorMessage(string errorClass, string errorMethod, string message)
        {
            oldApplication.SetErrorFlag();
            oldApplication.ErrorMessage = $"{errorClass} -> {errorMethod}: {message}";
        }

        /// <summary>
        /// Sets the status string and flag in Application.
        /// </summary>
        /// <param name="message">The status message.</param>
        public static void SetStatusMessage(string statusClass, string statusMethod, string message)
        {
            oldApplication.SetStatusFlag();
            oldApplication.StatusMessage = $"{statusClass} -> {statusMethod}: {message}";
        }
    }
}
