using UnityEngine;
using Zenject;

public class InventorySlotViewFactory 
{
    private readonly DiContainer _container;
    
    public InventorySlotViewFactory(DiContainer container)
    {
        _container = container;
    }
  
    public InventorySlotView Create(GameObject prefab, Transform parent)
    {
        var slotUI = _container.InstantiatePrefabForComponent<InventorySlotView>(prefab);
        var slotTransform = slotUI.transform;
        slotTransform.SetParent(parent);
        slotTransform.localScale = Vector3.one;
        return slotUI;
    }
}