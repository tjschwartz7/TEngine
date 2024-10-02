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
    internal class SaveFileHandler
    {
        private static SaveFile _currentSave;
        private static List<SaveFile> _saves;
        private static string _filePath;

        public static SaveFile CurrentSave
        {
            get => _currentSave;
            set => _currentSave = value;
        }

        public static List<SaveFile> Saves
        {
            get => _saves;
        }
        public static string FilePath { get => _filePath; set => _filePath = value; }

        public static void SetCurrentSave(int index)
        {
            _currentSave = _saves.ElementAt(index);
        }

        /// <summary>
        /// Creates a new file and returns the file name.
        /// </summary>
        /// <returns>The name of the newly created file.</returns>
        public static string CreateNewSave()
        {
            if(_saves == null) { _saves = new List<SaveFile>(); }
            int id = _saves.Count;
            string fileName = "save" + id;
            _currentSave = new SaveFile(id, fileName);
            _saves.Add(_currentSave);
            return fileName;
        }

        public static void AddSaveData(string key, string value)
        {
            _currentSave.AddSaveData(key, value);
        }
        public static void RemoveSaveData(string key)
        {
            _currentSave.RemoveSaveData(key);
        }

        public static string GetSaveData(string key)
        {
            return _currentSave.GetSaveData(key);
        }

        public static void SaveCurrentFile()
        {
            if(_filePath == null) { MessageUtils.SetErrorMessage("SaveFileHandler", "SaveCurrentFile", "Attempted to save file without setting a path!!"); }
            _filePath = _filePath + _currentSave.FileName + ".json";
            try
            {
                // Serialize the object to JSON
                string jsonString = JsonSerializer.Serialize(_currentSave);

                // Write the JSON string to the file
                File.WriteAllText(_filePath, jsonString);
                MessageUtils.SetStatusMessage("SaveFileHandler", "SaveCurrentFile", "Save file created successfully.");
            }
            catch (Exception ex)
            {
                // Handle any errors that might occur
                MessageUtils.SetErrorMessage("SaveFileHandler", "SaveCurrentFile", $"An error occurred: {ex.Message}");
            }
        }


        public static SaveFile ReadFile(string filePath)
        {
            try
            {
                // Read the JSON file content
                string jsonString = File.ReadAllText(filePath);

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

        public static void InitializeSaveFiles()
        {
            if (_filePath == null) { _filePath = Directory.GetCurrentDirectory() + "../../../../SaveEngine/Saves/"; }
            _saves = new List<SaveFile>();
            try
            {
                // Get all files in the specified directory
                foreach (string filePath in Directory.EnumerateFiles(_filePath))
                {
                    _saves.Add(ReadFile(filePath));
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that might occur
                MessageUtils.SetErrorMessage("SaveFileHandler", "InitializeSaveFiles", $"An error occurred: {ex.Message}");
            }
        }

        public static string GetSaveString()
        {
            string sout = "";
            sout += $"{_currentSave.Id} - {_currentSave.FileName}\n";
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
