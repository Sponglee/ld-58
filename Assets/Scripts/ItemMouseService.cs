using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class ItemMouseService: ITickable, IInitializable, IDisposable
{
    public event Action OnHandCanceled;
    public event Action OnScroll;

    private GameUIController _gameUIController;
    private InputAction _mouseFollowAction;
    private InputAction _mouseRightClickAction;
    private InputAction _mouseScrollAction;

    private bool _isHandEmpty = true;

    public float CameraScrollInput { get; private set; }
    
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
        _mouseScrollAction = map.FindAction("MouseScroll");
    }

    public void Initialize()
    {
        _mouseRightClickAction.performed += ctx => RightClick();
        _mouseScrollAction.performed += ctx => ScrollHandler();
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

    public void Rotatehand(bool isClockwise)
    {
        _gameUIController.RotateHand(isClockwise);
    }

    public void GrabHand(InventoryItemData data, float duration, Ease ease)
    {
        _gameUIController.ActivateHand(data, duration, ease);
        _isHandEmpty = false;
    }
    
    public void EmptyHand()
    {
        _isHandEmpty = true;
        _gameUIController.DeactivateHand();
    }
    
    public bool IsHandEmpty()
    {
        return _isHandEmpty;
    }

    private void RightClick()
    {
        OnHandCanceled?.Invoke();
    }
    
    private void ScrollHandler()
    {
        CameraScrollInput = _mouseScrollAction.ReadValue<Vector2>().y;
        OnScroll?.Invoke();
    }


  
}