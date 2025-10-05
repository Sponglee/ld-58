using System;
using System.Collections.Generic;
using System.Linq;
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

    public bool CheckSlot(InventorySlotController slot, InventoryItemData handData, string shape,
        out List<InventorySlotController> affectedSlots)
    {
        affectedSlots = new List<InventorySlotController>();
        var slotCoord = slot.GetSlotCoords();

        var lines = shape.Split("\n");
        var lineLength = lines.Length;
        var shapeSlotCount = 0;
        for (var y = 0; y < lines.Length; y++)
        {
            for (var x = 0; x < lineLength; x++)
            {
                var checkCoord = new Vector2(x + slotCoord.x - 1, y + slotCoord.y - 1);
                var isHandShape = lines[y][x] == '1';
                
                if (isHandShape)
                {
                    shapeSlotCount++;
                }
                
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

        if (affectedSlots.Count < shapeSlotCount)
        {
            affectedSlots.Clear();
            return false;
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
    private Transform _t;
    public Transform Transform => _t ??= _view.gameObject.transform;

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
        _model.SetEmpty(false);
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
    
    public void SetEmpty(bool toggle)
    {
        _isEmpty = toggle;
    }
}

[Serializable]
public class InventoryItemData
{
    public InventoryItemType type;
    [TextArea(3, 3)] public string shape;
    public float ScoreValue;
    public Sprite inventoryIcon;
    
    public string RotateShape(string shape)
    {
        var rows = shape.Trim().Split('\n').Select(r => r.ToCharArray()).ToArray();
    
        var rotated = Enumerable.Range(0, rows[0].Length)
            .Select(col => new string(
                Enumerable.Range(0, rows.Length)
                    .Select(row => rows[rows.Length - 1 - row][col])
                    .ToArray()
            ));
    
        return string.Join("\n", rotated);
    }
    
    public string RotateShapeCounterClockwise(string shape)
    {
        var rows = shape.Trim().Split('\n').Select(r => r.ToCharArray()).ToArray();
    
        var rotated = Enumerable.Range(0, rows[0].Length)
            .Reverse()  // Read columns in reverse order
            .Select(col => new string(
                Enumerable.Range(0, rows.Length)
                    .Select(row => rows[row][col])  // Read top to bottom
                    .ToArray()
            ));
    
        return string.Join("\n", rotated);
    }
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