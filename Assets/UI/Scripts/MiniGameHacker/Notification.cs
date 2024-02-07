using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notification : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _description;

    public void WriteTrueSymbols(string text)
    {
        _description.text = "Правильные символы: " + text;
    }
}
