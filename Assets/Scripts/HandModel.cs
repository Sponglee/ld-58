using System;
using UnityEngine.Rendering;

namespace DefaultNamespace
{
    public class HandModel : IDisposable
    {
        private InventoryItemData _data;
        
        public InventoryItemData InventoryData => _data;
        public HandModel()
        {
        }

        public void Dispose()
        {
            _data = null;
        }
    }
}