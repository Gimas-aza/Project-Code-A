using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.StorageService
{
    public class JsonToFileStorageService : ISerializable
    {
        private bool _isInProgress = false;

        public void Save(string key, object value, Action<bool> callback = null)
        {
            if (!_isInProgress)
            {
                SaveAsync(key, value, callback);
            }
            else
            {
                callback?.Invoke(false);
            }
        } 

        public void Load<T>(string key, Action<T> callback)
        {
            string path = BuildPath(key);

            using (var fileStream = new StreamReader(path))
            {
                var json = fileStream.ReadToEnd();
                var date = JsonConvert.DeserializeObject<T>(json);

                callback?.Invoke(date);
            }
        }

        private async void SaveAsync(string key, object value, Action<bool> callback)
        {
            _isInProgress = true;
            string path = BuildPath(key);
            string json = JsonConvert.SerializeObject(value, Formatting.Indented); 

            using (var fileStream = new StreamWriter(path))
            {
                await fileStream.WriteAsync(json);
            }

            _isInProgress = false;
            callback?.Invoke(true);
        }

        private string BuildPath(string key)
        {
            return Path.Combine(Application.persistentDataPath, key + ".json");
        }
    }
}
