using System;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class InventoryService
{
    public event Action<InventorySlotController> OnCellClicked;

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

    public bool CheckSlot(InventorySlotController slot, InventoryItemData handData,
        out List<InventorySlotController> affectedSlots)
    {
        affectedSlots = new List<InventorySlotController>();
        var shape = handData.shape;
        var slotCoord = slot.GetSlotCoords();

        var line = shape.Split("\n");
        var lineLength = line.Length;

        for (var y = 0; y < line.Length; y++)
        {
            for (var x = 0; x < lineLength; x++)
            {
                var checkCoord = new Vector2(x + slotCoord.x - 1, y + slotCoord.y - 1);
                var isHandShape = line[y][x] == '1';
                var targetSlot = GetSlotByCoord(checkCoord);
                if (targetSlot == null )
                {
                    if (isHandShape)
                    {
                        return false;
                    }
                    
                    continue;
                }

                if (targetSlot.IsEmpty() && isHandShape)
                {
                    affectedSlots.Add(targetSlot);
                }
            }
        }

        return true;
    }

    public void SetUpSlots(List<InventorySlotController> slots)
    {
        foreach (var slot in _slots)
        {
            slot.OnSlotClicked -= SlotSelectHandler;
        }

        _slots.Clear();
        _slots = slots;

        foreach (var slot in _slots)
        {
            slot.OnSlotClicked += SlotSelectHandler;
        }
    }

    private void SlotSelectHandler(InventorySlotController obj)
    {
        OnCellClicked?.Invoke(obj);
    }

    private InventorySlotController GetSlotByCoord(Vector2 coord)
    {
        foreach (var slot in _slots)
        {
            var slotCoords = slot.GetSlotCoords();
            if (slotCoords == coord)
            {
                return slot;
            }
        }

        return null;
    }
}

public class InventorySlotController : IDisposable
{
    public event Action<InventorySlotController> OnSlotClicked;

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
        _view.OnSlotButtonPressed += SlotButtonClickHandler;
    }

    public void Dispose()
    {
        _view.OnSlotButtonPressed -= SlotButtonClickHandler;
    }

    public Vector2 GetSlotCoords()
    {
        return _model.Coords;
    }

    public bool IsEmpty()
    {
        return _model.IsEmpty;
    }

    public void FillSlot(InventoryItemData data)
    {
        _view.SetImage(data.inventoryIcon);
    }

    private void SlotButtonClickHandler()
    {
        OnSlotClicked?.Invoke(this);
    }
}

public class InventorySlotModel
{
    public InventorySlotModel(Vector2 coords, bool isEmpty)
    {
        _isEmpty = isEmpty;
        _coords = coords;
    }

    private bool _isEmpty;

    private Vector2 _coords;

    public Vector2 Coords => _coords;
    public bool IsEmpty => _isEmpty;
}

[Serializable]
public class InventoryItemData
{
    public InventoryItemType type;
    [TextArea(3, 3)] public string shape;
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