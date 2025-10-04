using System;
using Zenject;

public class GameUIProvider: IInitializable, IDisposable
{
    private GameStateService _gameStateService;
    private GameUIController _gameUIController;
    
    public GameUIProvider(GameUIController gameUIController, GameStateService gameStateService)
    {
        _gameStateService = gameStateService;
        _gameUIController = gameUIController;
    }

    public void Initialize()
    {
        _gameUIController.ToggleUI(false);

        _gameStateService.OnGameStateChanged += StateChangeHandler;
        _gameUIController.OnPauseButtonPressed += PauseGame;

    }

    public void Dispose()
    {
        _gameStateService.OnGameStateChanged -= StateChangeHandler;
        _gameUIController.OnPauseButtonPressed += PauseGame;
    }
    
    private void StateChangeHandler(GameState gameState)
    {
        var toggleState = gameState == GameState.Play;
        _gameUIController.ToggleUI(toggleState);
    }
    
    private void PauseGame()
    {
        _gameStateService.ChangeState(GameState.Pause);
    }
}