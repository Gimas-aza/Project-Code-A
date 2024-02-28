using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.StorageService
{
    public class StorageService : MonoBehaviour
    {
        [SerializeField] private string _key = "test";

        private ISerializable _storageService = new BinaryToFileStorageService();
        private Dictionary<IDataStorage, object[]> _dataStorages = new();

        public void AddDataStorage(IDataStorage dataStorage)
        {
            _dataStorages.Add(dataStorage, null);
        }

        [ContextMenu("Save")]
        public void SaveGame()
        {
            _dataStorages = _dataStorages.Keys.ToDictionary(dataStorage => dataStorage, dataStorage => dataStorage.SaveDate());
            _storageService.Save(_key, _dataStorages, (_) => Debug.Log("Success"));
        }

        [ContextMenu("Load")]
        public void LoadGame()
        {
            _storageService.Load<Dictionary<IDataStorage, object[]>>(_key, (data) => _dataStorages = data);
            foreach (var dataStorage in _dataStorages.Keys)
            {
                dataStorage.LoadDate(_dataStorages[dataStorage]);
            }
        }
    }
}