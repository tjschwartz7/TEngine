using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Helpers
{
    internal static class MessageUtils
    {
        /// <summary>
        /// Prints to stderr and terminates the program.
        /// </summary>
        /// <param name="error">The error message.</param>
        public static void TerminateWithError(string errorClass, string errorMethod, string message)
        {
            Application.ApplicationInstance.SetErrorFlag();
            TextWriter errorWriter = Console.Error;
            errorWriter.WriteLine($"{errorClass} -> {errorMethod}: {message}");
            Application.TerminateApplication();
        }

        /// <summary>
        /// Sets the Application error message and flag. Does not terminate the program.
        /// </summary>
        /// <param name="message">The error message.</param>
        public static void SetErrorMessage(string errorClass, string errorMethod, string message)
        {
            Application.ApplicationInstance.SetErrorFlag();
            Application.ApplicationInstance.ErrorMessage = $"{errorClass} -> {errorMethod}: {message}";
        }

        /// <summary>
        /// Sets the status string and flag in Application.
        /// </summary>
        /// <param name="message">The status message.</param>
        public static void SetStatusMessage(string statusClass, string statusMethod, string message)
        {
            Application.ApplicationInstance.SetStatusFlag();
            Application.ApplicationInstance.StatusMessage = $"{statusClass} -> {statusMethod}: {message}";
        }
    }
}
