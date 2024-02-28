using System.Collections;
using System.Collections.Generic;
using Assets.StorageService;
using UnityEngine;

public class ExampleObject : MonoBehaviour, IDataStorage
{
    [SerializeField] private StorageService _storageService;
    [Header("Data")]
    [SerializeField] private int _counter;
    [SerializeField] private bool _isTest;
    [SerializeField] private string _name;

    private void Start()
    {
        _storageService?.AddDataStorage(this);
    }

    public object[] SaveDate()
    {
        var data = new object[]
        {
            _counter,
            _isTest,
            _name
        };

        return data;
    }

    public void LoadDate(object[] value)
    {
        _counter = (int)value[0];
        _isTest = (bool)value[1];
        _name = (string)value[2];
        Debug.Log("Counter: " + _counter + " IsTest: " + _isTest + " Name: " + _name);
    }
}
