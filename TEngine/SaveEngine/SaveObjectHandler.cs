using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TEngine.Helpers;

namespace TEngine.SaveEngine
{
    //T must be a class type
    public class SaveObjectHandler<T> where T : class
    {
        private string _filePath;
        private string _fileName;
        public SaveObjectHandler(string path, string filename)
        {
            _filePath = path;
            _fileName = filename;
        }

        /// <summary>
        /// Attempts to save the generic object as a JSON file. 
        /// </summary>
        /// <param name="saveObject">The object to be serialized and saved.</param>
        /// <returns>True if the file was successfully saved.</returns>
        public bool SaveObject(T saveObject)
        {
            try
            {
                // Serialize the object to JSON
                string jsonString = JsonSerializer.Serialize(saveObject);
                // Write the JSON string to the file
                File.WriteAllText(_filePath+_fileName, jsonString);
                MessageUtils.SetStatusMessage("SaveFileHandler", "SaveCurrentFile", "Save file created successfully.");
                return true;
            }
            catch (Exception ex)
            {
                // Handle any errors that might occur
                MessageUtils.SetErrorMessage("SaveFileHandler", "SaveCurrentFile", $"An error occurred: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Attempts to read a save file. Returns null if none exists.
        /// </summary>
        /// <returns>The deserialized object, or null if none was found.</returns>
        public T ReadObjectFile()
        {
            try
            {
                // Read the JSON file content
                string jsonString = File.ReadAllText(_filePath + _fileName);

                // Deserialize the JSON string to a Person object
                T save = JsonSerializer.Deserialize<T>(jsonString);
               
                return save;

            }
            catch (Exception ex)
            {
                // Handle any errors that might occur
                MessageUtils.SetErrorMessage("SaveFileHandler", "SaveCurrentFile", $"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
