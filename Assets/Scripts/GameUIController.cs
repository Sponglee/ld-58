
using System;
using UnityEngine;
using Zenject;

public class GameUIController: IInitializable, IDisposable
{
    public event Action OnPauseButtonPressed;
    
    private GameUIView _view;
    private GameUIModel _model;
    
    public GameUIController(GameUIView view, GameUIModel model)
    {
        _view = view;
        _model = model;
    }

    public void Initialize()
    {
        _view.OnPauseButtonPressed += PausePressed;
    }

    public void Dispose()
    {
        _view.OnPauseButtonPressed -= PausePressed;
    }
    
    public void ToggleUI(bool toggleState)
    {
        _view.gameObject.SetActive(toggleState);
    }

    private void PausePressed()
    {
        OnPauseButtonPressed?.Invoke();
    }

    public Transform GetInventoryParent()
    {
        return _view.GetInventoryParent();
    }

    public void SetWorldPosition(Vector2 mousePos)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _view.GetRectTransform(), 
            mousePos,
            null,
            out Vector2 localPoint
        );
        
        _view.SetHandPosition(localPoint);
    }

    public void ActivateHand(InventoryItemData data)
    {
        _view.InitializeHand(data);
    }

    public void DeactivateHand()
    {
        _view.DeinitializeHand();
        // _view.SetHandPosition(_view.GetRectTransform().position);
    }
}