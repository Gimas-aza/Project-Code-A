using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Units.Player.Inventory
{
    public class InventoryTableView : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [Header("Tags")]
        [SerializeField] private List<RectTransform> _tags;
        [Header("View")]
        [SerializeField] private ItemView _itemView;
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _weight;
        [SerializeField] private PopUpDropItems _popUpDropItems;
        [Header("Input")]
        [SerializeField] private Button _deleteItem;
        
        private InventoryTable _inventoryTable;
        private List<ItemView> _itemViews = new();
        private Item _currentItem;

        private void Start()
        {
            _inventoryTable = _inventory.InventoryTable;
            _inventoryTable.ItemsAdded += ViewAddedItem;
            _inventoryTable.ItemsRemoved += RemoveItemView;
            _popUpDropItems.Accept.onClick.AddListener(() => DeleteItem(_currentItem, _popUpDropItems.CurrentDropItems));
        }

        private void OnDisable()
        {
            _inventoryTable.ItemsAdded -= ViewAddedItem;
        }

        private void ViewAddedItem(Item item, int amount)
        {
            _tags[(int)item.Tag].gameObject.SetActive(true);
            AddItemView(item, amount);
        }

        private void AddItemView(Item item, int amount)
        {
            foreach (var itemV in _itemViews)
            {
                if (itemV.Id == item.ItemId)
                {
                    itemV.Init(item, amount);
                    return;
                }
            }

            var itemView = Instantiate(_itemView, _tags[(int)item.Tag]);
            itemView.Init(item, amount);
            itemView.OnClick.AddListener(ShowItemInformation);
            _itemViews.Add(itemView);

            var tag = _tags[(int) item.Tag];
            var contentSize = tag.GetComponent<ContentSizeFitter>();

            contentSize.enabled = false;
            tag.sizeDelta = new Vector2(tag.sizeDelta.x, 40 + 7);
        }

        private void ShowItemInformation(Item item)
        {
            _itemIcon.sprite = item.Icon;
            _description.text = item.Description;
            _weight.text = item.ItemWeight.ToString();

            _popUpDropItems.Init(_inventoryTable.GetAmount(item.ItemId));
            _deleteItem.onClick.AddListener(_popUpDropItems.ShowPopUp);
            _currentItem = item;
        }

        private void DeleteItem(Item item, int amount)
        {
            Debug.Log(amount);
            _inventoryTable.RemoveItems(item.ItemId, amount);
        }

        private void RemoveItemView(Item item, int amount)
        {
            foreach (var itemV in _itemViews)
            {
                if (itemV.Id == item.ItemId)
                {
                    var currentAmount = itemV.Amount - amount;
                    if (currentAmount == 0)
                    {
                        _deleteItem.onClick.RemoveAllListeners();            
                        

                        _itemViews.Remove(itemV);
                        StartCoroutine(ViewRemoveItem(itemV));
                        return;
                    }

                    itemV.Init(item, -amount);
                    return;
                }
            }

            _popUpDropItems.Init(_inventoryTable.GetAmount(item.ItemId));
        }

        private IEnumerator ViewRemoveItem(ItemView itemV) 
        {
            var tag = _tags[(int) itemV.Item.Tag];
            tag.sizeDelta = new Vector2(tag.sizeDelta.x, 0);
            Destroy(itemV.gameObject);

            yield return null;
            var contentSize = tag.GetComponent<ContentSizeFitter>();
            contentSize.enabled = true;
        }
    }
}
