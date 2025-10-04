    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class InventoryService
    {
        private LinkedList<InventoryItem> _inventoryItems = new LinkedList<InventoryItem>();
        private List<InventorySlotController> _slots = new List<InventorySlotController>();

        public void RemoveFromInventory(InventoryItem item)
        {
            _inventoryItems.Remove(item);
        }

        public void AddToInventory(InventoryItemData data)
        {
            var item = new InventoryItem(data);
            
        }

        public void SetUpSlots(List<InventorySlotController> slots)
        {
            _slots.Clear();
            _slots = slots;
        }
    }

    public class InventorySlotController
    {
        private InventorySlotView _view;
        private InventorySlotModel _model;
        
        public InventorySlotController(
            InventorySlotModel model,
            InventorySlotView view)
        {
            _view = view;
            _model = model;
        }

        public void Initialize()
        {
            // _view = view;
        }
    }

    public class InventorySlotModel
    {
        private InventoryItem _storedItem;

    }
    [Serializable]
    public class InventoryItemData
    {
        public InventoryItemType type;
        public float ScoreValue;
        public Sprite inventoryIcon;
    }
    
    public class InventoryItem
    {
        private InventoryItemData _data;

        public InventoryItem(InventoryItemData data)
        {
            _data = data;
        }
    }

    public enum InventoryItemType
    {
        Ball,
        Vaze,
        Staff
    }
