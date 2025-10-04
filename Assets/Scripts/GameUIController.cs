
using System;
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
}