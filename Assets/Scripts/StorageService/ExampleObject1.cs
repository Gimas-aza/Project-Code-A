using System.Collections;
using System.Collections.Generic;
using Assets.StorageService;
using UnityEngine;

public class ExampleObject1 : MonoBehaviour, IDataStorage
{
    [SerializeField] private StorageService _storageService;

    private void Start()
    {
        _storageService?.AddDataStorage(this);
    }

    public object[] SaveDate()
    {
        var data = new object[]
        {
            transform.position,
        };

        return data;
    }

    public void LoadDate(object[] value)
    {
        transform.position = (Vector3)value[0];
        Debug.Log(transform.position);
    }
}
