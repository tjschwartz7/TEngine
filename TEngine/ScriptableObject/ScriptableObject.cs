using TEngine.Utils.Logging;
using System.Text.Json;

namespace TEngine.ScriptableObject
{

    public abstract class ScriptableObject<T> where T : ScriptableObject<T>, new()
    {
        public virtual void OnEnable() { }  // Called when loaded
        public virtual void OnDisable() { } // Called when unloaded

        public void Save(string fileName)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            string json = JsonSerializer.Serialize(this, GetType(), new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
            Logging.Info($"Saved {GetType().Name} to {path}");
        }

        public static T Load<T>(string fileName) where T : ScriptableObject<T>, new()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                T obj = JsonSerializer.Deserialize<T>(json);
                obj?.OnEnable();
                Logging.Info($"Loaded {typeof(T).Name} from {path}");
                return obj;
            }

            Logging.Error($"No save file found for {typeof(T).Name}");
            return new T(); // Return default instance if file doesn't exist
        }

        public static T CreateInstance()
        {
            T instance = new T();
            instance.OnEnable();
            return instance;
        }
    }
}
