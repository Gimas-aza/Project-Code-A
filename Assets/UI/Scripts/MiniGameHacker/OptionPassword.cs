using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OptionPassword : MonoBehaviour, IPointerClickHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private TextMeshProUGUI _text;

    private string _textValue;

    public string TextValue => _textValue;

    public bool TruePassword;
    public UnityAction<OptionPassword> OnClick;

    public void Init()
    {
        _textValue = CreatePassword(8);
    }

    public void OnSelect(BaseEventData eventData)
    {
        _text.color = new Color(0, 0.556f, 0.05f);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _text.color = new Color(0, 0, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke(this);
        if (TruePassword)
        {
            Debug.Log("Win");
        }
    }

    private string CreatePassword(int length)
    {
        const string valid = "ABCDEFGHIJKLM";
        StringBuilder res = new();
        System.Random rnd = new();
        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }
        return res.ToString();
    }

    public void ShowPassword()
    {
        _text.text = _textValue;
    }

}
