using System;using Zenject;

public class ShopController: IDisposable
{
    public event Action OnGameStartInput;
    public event Action<UpgradeType> OnUpgradeInput;

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
        _view.OnUpgradePressed += Upgrade;
    }

    public void Dispose()
    {
        _view.OnStartButtonPressed -= StartGame;
        _view.OnUpgradePressed -= Upgrade;
    }
    
    public void ToggleUI(bool toggleState)
    {
        _view.gameObject.SetActive(toggleState);
    }
    
    private void Upgrade(UpgradeType upgradeType)
    {
        OnUpgradeInput?.Invoke(upgradeType);
    }

    private void StartGame()
    {
        OnGameStartInput?.Invoke();
    }

    public void UpgradeVisuals(UpgradeData data)
    {
        _view.UpdateUpgrade(data);
    }

    public void UpgradeVisual(UpgradeData data)
    {
        _view.UpdateUpgrade(data);
    }
}