using System;using Zenject;

public class MainMenuController: IInitializable, IDisposable
{
    public event Action OnGameStartInput;
    public event Action OnCreditsInput;

    private MainMenuView _view;
    private MainMenuModel _model;
    
    public MainMenuController(MainMenuView menuView, MainMenuModel model)
    {
        _view = menuView;
        _model = model;
    }
    
    public void Initialize()
    {
        _view.OnStartButtonPressed += StartGame;
        _view.OnCreditsButtonPressed += CreditsOpen;
    }

    public void Dispose()
    {
        _view.OnStartButtonPressed -= StartGame;
        _view.OnCreditsButtonPressed -= CreditsOpen;
    }
    
    public void ToggleUI(bool toggleState)
    {
        _view.gameObject.SetActive(toggleState);
    }

    private void StartGame()
    {
        OnGameStartInput?.Invoke();
    }
    
    private void CreditsOpen()
    {
        OnCreditsInput?.Invoke();
    }


}