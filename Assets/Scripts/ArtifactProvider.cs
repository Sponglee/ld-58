using System;
using Zenject;

public class ArtifactProvider : IInitializable, ITickable, IDisposable
{
    public event Action<Artifact> OnArtifactPickedUp;
    
    private GamePreset _gamePreset;
    private GameStateService _gameStateService;
    private GameUIController _gameUIController;
    private ArtifactManager _artifactManager;
    private ItemMouseService _itemMouseService;
    
    public ArtifactProvider(
        GameUIController gameUIController,
        GamePreset gamePreset,
        GameStateService gameStateService,
        ArtifactManager artifactManager,
        ItemMouseService itemMouseService)
    {
        _gamePreset = gamePreset;
        _gameStateService = gameStateService;
        _gameUIController = gameUIController;
        _artifactManager = artifactManager;
        _itemMouseService = itemMouseService;
    }

    public void Initialize()
    {
        _artifactManager.OnArtifactTriggered += ArtifactTriggered;
        _gameStateService.OnGameStateChanged += GameStateHandler;
    }

    private void ArtifactTriggered(Artifact obj)
    {
        if (_gameStateService.GameState != GameState.Play)
        {
            return;
        }

        if (!_itemMouseService.IsHandEmpty())
        {
            return;
        }
        
        OnArtifactPickedUp?.Invoke(obj);
    }

    public void Dispose()
    {
        _artifactManager.OnArtifactTriggered -= ArtifactTriggered;

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