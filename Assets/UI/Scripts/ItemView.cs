using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Units.Player.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;

    private int _amount;
    private Item _item;
    private Button _button;

    public string Id { get; private set; }
    public int Amount => _amount;
    public Item Item => _item;
    public UnityEvent<Item> OnClick;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => OnClick?.Invoke(_item));
    }
    
    private void OnDestroy()
    {
        OnClick.RemoveAllListeners();
    }

    public void Init(Item item, int amount)
    {
        Id = item.ItemId;
        _item = item;
        _amount += amount;
        _name.text = item.Name + " " + "(" + _amount + ")";
    }

}
