using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TEngine.Helpers;


namespace TEngine.SaveEngine
{
    public class SaveFileHandler
    {
        private static SaveFile _currentSave;
        private static string _filePath;
        private static string _fileName;

        public SaveFileHandler(string path, string filename)
        {
            _filePath = path;
            _fileName = filename;
            _currentSave = new SaveFile();
            _currentSave.FileName = filename;
        }


        public void AddSaveData(string key, string value)
        {
            _currentSave.AddSaveData(key, value);
        }
        public void RemoveSaveData(string key)
        {
            _currentSave.RemoveSaveData(key);
        }

        /// <summary>
        /// Attempts to retrieve the save data. Returns empty string if not found.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetSaveData(string key)
        {
            return _currentSave.GetSaveData(key);
        }

        public void SaveCurrentFile()
        {
            try
            {
                // Serialize the object to JSON
                string jsonString = JsonSerializer.Serialize(_currentSave);

                // Write the JSON string to the file
                File.WriteAllText(_filePath+_fileName, jsonString);
                MessageUtils.SetStatusMessage("SaveFileHandler", "SaveCurrentFile", "Save file created successfully.");
            }
            catch (Exception ex)
            {
                // Handle any errors that might occur
                MessageUtils.SetErrorMessage("SaveFileHandler", "SaveCurrentFile", $"An error occurred: {ex.Message}");
            }
        }


        private SaveFile ReadFile()
        {
            try
            {
                // Read the JSON file content
                string jsonString = File.ReadAllText(_filePath+_fileName);

                // Deserialize the JSON string to a Person object
                SaveFile save = JsonSerializer.Deserialize<SaveFile>(jsonString);
                return save;

            }
            catch (Exception ex)
            {
                // Handle any errors that might occur
                MessageUtils.SetErrorMessage("SaveFileHandler", "SaveCurrentFile", $"An error occurred: {ex.Message}");
                return null;
            }
        }

        public void InitializeSaveFiles()
        {
            try
            {
                _currentSave = ReadFile();
                if (_currentSave == null)
                {
                    _currentSave = new SaveFile();
                    _currentSave.FileName = _fileName;
                }
                   
                SaveCurrentFile();
            }
            catch (Exception ex)
            {
                // Handle any errors that might occur
                MessageUtils.SetErrorMessage("SaveFileHandler", "InitializeSaveFiles", $"An error occurred: {ex.Message}");
            }
        }

        public string GetSaveString()
        {
            string sout = "";
            sout += $"{_currentSave.FileName}\n";
#if DEBUG
            foreach (var data in _currentSave.SaveData)
            {
                sout += $"{data.Key} - {data.Value}\n";
            }
#endif
            return sout;
        }
    }
}
