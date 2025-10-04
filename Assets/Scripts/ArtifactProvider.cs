using System;
using Zenject;

public class ArtifactProvider : IInitializable, ITickable, IDisposable
{
    private GamePreset _gamePreset;
    private GameStateService _gameStateService;
    private GameUIController _gameUIController;
    

    public ArtifactProvider(
        GameUIController gameUIController,
        GamePreset gamePreset,
        GameStateService gameStateService)
    {
        _gamePreset = gamePreset;
        _gameStateService = gameStateService;
        _gameUIController = gameUIController;
    }

    public void Initialize()
    {
        _gameStateService.OnGameStateChanged += GameStateHandler;
    }

    public void Dispose()
    {
        _gameStateService.OnGameStateChanged -= GameStateHandler;
    }

    public void Tick()
    {
        
    }
        
    private void GameStateHandler(GameState state)
    {
        // _isLevelMoving = state == GameState.Play;
    }
}