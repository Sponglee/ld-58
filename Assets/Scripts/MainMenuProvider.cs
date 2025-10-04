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
        _mainMenuController.OnGameStartInput += StartGame;
    }
    
    public void Dispose()
    {
        _gameStateService.OnGameStateChanged -= StateChangeHandler;
        _mainMenuController.OnGameStartInput -= StartGame;

    }

    private void StartGame()
    {
        _gameStateService.ChangeState(GameState.Play);
    }

    private void StateChangeHandler(GameState gameState)
    {
        var toggleState = gameState != GameState.Play;
        _mainMenuController.ToggleUI(toggleState);
    }


}