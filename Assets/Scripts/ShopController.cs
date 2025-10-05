using System;using Zenject;

public class ShopController: IInitializable, IDisposable
{
    public event Action OnGameStartInput;
    
    private ShopView _view;
    private ShopModel _model;
    
    public ShopController(ShopView menuView, ShopModel model)
    {
        _view = menuView;
        _model = model;
    }
    
    public void Initialize()
    {
        _view.OnStartButtonPressed += StartGame;
    }

    public void Dispose()
    {
        _view.OnStartButtonPressed -= StartGame;
    }
    
    public void ToggleUI(bool toggleState)
    {
        _view.gameObject.SetActive(toggleState);
    }

    private void StartGame()
    {
        OnGameStartInput?.Invoke();
    }


}