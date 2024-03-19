using UnityEngine.InputSystem;
using Assets.Units.Base;
using UnityEngine;
using Zenject;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Units.Player.Inventory
{
    public class Inventory : OverlapBase
    {
        [SerializeField] private InventoryTableData _data = new();
        [SerializeField] private Transform _storageItems;

        private InputSystem _inputSystem;
        private InputAction _inputAction;
        private List<Item> _allItems;

        public InventoryTable InventoryTable;

        [Inject]
        private void Constructor(InputSystem input)
        {
            _inputSystem = input;
        }

        private void Awake()
        {
            _allItems = Resources.LoadAll<Item>("Items").ToList();
            InventoryTable = new InventoryTable(_data);
            _inputAction = _inputSystem.Player.Interaction;
        }

        private void OnEnable()
        {
            _inputAction.performed += Interaction;
            InventoryTable.ItemsRemoved += PullOutStorage;
        }

        private void OnDisable()
        {
            _inputAction.performed -= Interaction;
            InventoryTable.ItemsRemoved -= PullOutStorage;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                var itemAmount = InventoryTable.GetAmount("Disk");
                InventoryTable.RemoveItems("Disk", itemAmount);
            }
        }

        private void Interaction(InputAction.CallbackContext context)
        {
            StartAction();
        }

        public override void StartAction()
        {
            if (TryFind())
            {
                TryAction();
            }
        }

        protected override void TryAction()
        {
            for (int i = 0; i < OverlapResultsCount; i++)
            {
                if (OverlapResults[i].TryGetComponent(out Item item) == false)
                {
                    continue;
                }
                
                if (CheckObstacle(OverlapResults[i]))
                {
                    continue;
                }

                var result = InventoryTable.AddItems(item, 1);
                if (result.ItemsNotAddedAmount == 0)
                {
                    MoveStorage(item);
                }
            }
        }

        private void MoveStorage(Item item)
        {
            Destroy(item.gameObject);
        }

        private void PullOutStorage(Item item, int amount)
        {
            var itemSlot = _allItems
                .Where(i => i.ItemId == item.ItemId)
                .FirstOrDefault();
            Vector3 multiplierPos;

            for (var i = 0; i < amount; i++)
            {
                multiplierPos = new Vector3(Random.Range(-1, 5), Random.Range(-1, 5), Random.Range(-1, 5));

                Instantiate(itemSlot, _storageItems.position + multiplierPos, Quaternion.identity);
            }
        }
    }
}
