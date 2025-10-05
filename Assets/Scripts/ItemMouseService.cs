using System;
using DG.Tweening;
using Unity.Burst.Intrinsics;
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

    public float CameraScrollInput { get; private set; }
    public Vector2 MousePosition { get; private set; }
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
        var mousePos = _mouseFollowAction.ReadValue<Vector2>();

        MousePosition = _gameUIController.GetScreenPosition(mousePos);
      
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