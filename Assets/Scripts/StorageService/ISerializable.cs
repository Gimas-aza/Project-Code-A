using System;

namespace Assets.StorageService
{
    public interface ISerializable
    {
        void Save(string key, object value, Action<bool> callback = null);
        void Load<T>(string key, Action<T> callback);    
    }
}
