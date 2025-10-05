

    using System.Collections.Generic;
    using UnityEngine;

    public class CollectionService
    {
        private List<CollectionItemData> _collections = new List<CollectionItemData>();
        public CollectionService()
        {
            
        }

        public void RegisterCollection(CollectionItem itemView, InventoryItemData data)
        {
            var collectionData = new CollectionItemData(itemView, data);
            _collections.Add(collectionData);
        }

        public int GetCollectedAmount(InventoryItemType inventoryDataType)
        {
            foreach (var collection in _collections)
            {
                if (collection.InventoryItemData.type == inventoryDataType)
                {
                    return collection.CollectedAmount;
                }
            }

            return 0;
        }

        public void ItemCollected(InventoryItemData data)
        {
            foreach (var collection in _collections)
            {
                if (collection.InventoryItemData.type == data.type)
                {
                    collection.CollectedAmount++;
                }
            }
        }

        public CollectionItem GetCollectionItem(InventoryItemData inventoryItemData)
        {
            foreach (var collection in _collections)
            {
                if (collection.InventoryItemData.type == inventoryItemData.type)
                {
                    return collection.CollectionItem;
                }
            }

            return null;
        }
    }

    public class CollectionItemData
    {
        public CollectionItem CollectionItem { get; private set; }
        public InventoryItemData InventoryItemData { get; private set; }
        
        private int _collectedAmount;
        public int CollectedAmount
        {
            get
            {
                return PlayerPrefs.GetInt("Collection" + InventoryItemData.type, 0);
            }
            set
            {
                 PlayerPrefs.SetInt("Collection" + InventoryItemData.type, value);
                _collectedAmount = value; 
            }
        }


        public CollectionItemData(CollectionItem item, InventoryItemData data)
        {
            CollectionItem = item;
            InventoryItemData = data;
            _collectedAmount = CollectedAmount;
        }
    }
