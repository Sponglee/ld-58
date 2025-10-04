using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Input = UnityEngine.Windows.Input;

public class ItemMouseService: ITickable, IInitializable, IDisposable
{
    public event Action OnHandCanceled;
    
    private GameUIController _gameUIController;
    private InputAction _mouseFollowAction;
    private InputAction _mouseRightClickAction;

    private bool _isHandEmpty = true;
    
    public ItemMouseService(
        GameUIController gameUIcontroller,
        InputActionAsset inputActionAsset)
    {
        _gameUIController = gameUIcontroller;
        var input = inputActionAsset;
        var map = input.FindActionMap("Mouse");
        map.Enable();
        _mouseFollowAction = map.FindAction("MousePosition");
        _mouseRightClickAction = map.FindAction("RightClick");
    }

    public void Initialize()
    {
        _mouseRightClickAction.performed += ctx => RightClick();
    }

    public void Dispose()
    {
    }
    
    public void Tick()
    {
        if (_isHandEmpty)
        {
            return;
        } 
        
        var mousePos = _mouseFollowAction.ReadValue<Vector2>();
       
        _gameUIController.SetWorldPosition(mousePos);
    }

    public void GrabHand(InventoryItemData data)
    {
        _gameUIController.ActivateHand(data);
        _isHandEmpty = false;
    }
    
    public void EmptyHand()
    {
        _isHandEmpty = true;
        _gameUIController.DeactivateHand();
    }

    private void RightClick()
    {
        OnHandCanceled?.Invoke();
    }

    public bool IsHandEmpty()
    {
        return _isHandEmpty;
    }
}