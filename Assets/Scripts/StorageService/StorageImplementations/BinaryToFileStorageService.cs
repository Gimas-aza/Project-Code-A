using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using System.Threading.Tasks;

namespace Assets.StorageService
{
    public class BinaryToFileStorageService : ISerializable
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
            BinaryFormatter formatter = new BinaryFormatter();
            string path = BuildPath(key);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                var date = (T)formatter.Deserialize(fileStream);

                callback?.Invoke(date);
            }
        }

        private async void SaveAsync(string key, object value, Action<bool> callback)
        {
            _isInProgress = true;
            BinaryFormatter formatter = new BinaryFormatter();
            string path = BuildPath(key);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await Task.Run(() => formatter.Serialize(fileStream, value));
            }

            _isInProgress = false;
            callback?.Invoke(true);
        }

        private string BuildPath(string key)
        {
            return Path.Combine(Application.persistentDataPath, key + ".red6");
        }
    }
}