using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.StorageService
{
    public interface IDataStorage
    {
        object[] SaveDate();
        void LoadDate(object[] value);
    }
}