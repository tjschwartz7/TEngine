using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace TEngine.SaveEngine
{
    public class SaveFile
    {
        private string _fileName;

        private Dictionary<string, string> _saveData;

        public SaveFile() 
        {
        }
        public string FileName { get => _fileName; set => _fileName = value; }
        public Dictionary<string, string> SaveData { get => _saveData; set => _saveData = value; }
        /// <summary>
        /// Adds a new piece of save data with a given key.
        /// If a given key already exists within the file, it will be removed
        /// and added again.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddSaveData(string key, string value)
        {
            if (_saveData == null) _saveData = new Dictionary<string, string>();
            //Attempt to remove key (just in case it exists)
            _saveData.Remove(key);
            _saveData.Add(key, value);
        }

        public void RemoveSaveData(string key)
        {
            if (_saveData == null) _saveData = new Dictionary<string, string>();
            _saveData.Remove(key);
        }

        public string GetSaveData(string key) 
        {
            if (_saveData == null) _saveData = new Dictionary<string, string>();
            if (_saveData.ContainsKey(key))
                return _saveData[key];
            else return "";
        }

    }
}
