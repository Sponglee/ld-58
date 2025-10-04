using System;
using Zenject;

public class MainMenuProvider: IInitializable, IDisposable
{
    private GameStateService _gameStateService;
    private MainMenuController _mainMenuController;
    
    public MainMenuProvider(MainMenuController mainMenuController, GameStateService gameStateService)
    {
        _gameStateService = gameStateService;
        _mainMenuController = mainMenuController;
    }

    public void Initialize()
    {
        _mainMenuController.ToggleUI(true);
        
        _gameStateService.OnGameStateChanged += StateChangeHandler;
    }

    private void StateChangeHandler(GameState gameState)
    {
        var toggleState = gameState != GameState.Play;
        _mainMenuController.ToggleUI(toggleState);
    }

    public void Dispose()
    {
        _gameStateService.OnGameStateChanged -= StateChangeHandler;
    }
}