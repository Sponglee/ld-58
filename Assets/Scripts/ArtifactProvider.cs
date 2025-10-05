using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.XR;
using Zenject;

public class ArtifactProvider : IInitializable, ITickable, IDisposable
{
    public event Action<Artifact> OnArtifactPickedUp;
    
    private GamePreset _gamePreset;
    private GameStateService _gameStateService;
    private GameUIController _gameUIController;
    private ArtifactManager _artifactManager;
    private ItemMouseService _itemMouseService;
    private HandService _handService;
    private RunnerView _runner;
    
    public ArtifactProvider(
        GameUIController gameUIController,
        GamePreset gamePreset,
        GameStateService gameStateService,
        ArtifactManager artifactManager,
        ItemMouseService itemMouseService,
        HandService handService,
        RunnerView runnerView)
    {
        _gamePreset = gamePreset;
        _gameStateService = gameStateService;
        _gameUIController = gameUIController;
        _artifactManager = artifactManager;
        _itemMouseService = itemMouseService;
        _runner = runnerView;
        _handService = handService;
    }

    public void Initialize()
    {
        _artifactManager.OnArtifactTriggered += ArtifactTriggered;
        _handService.OnHandDropped += ArtifactDrop;
        _gameStateService.OnGameStateChanged += GameStateHandler;
    }

    public void Dispose()
    {
        _artifactManager.OnArtifactTriggered -= ArtifactTriggered;
        _handService.OnHandDropped -= ArtifactDrop;
        _gameStateService.OnGameStateChanged -= GameStateHandler;
    }

    public void Tick()
    {
        
    }
        
    private void GameStateHandler(GameState state)
    {
        // _isLevelMoving = state == GameState.Play;
    }
    
    private void ArtifactTriggered(Artifact obj)
    {
        if (_gameStateService.GameState != GameState.Play)
        {
            return;
        }

        if (!_handService.IsHandEmpty)
        {
            return;
        }
        
        OnArtifactPickedUp?.Invoke(obj);
        obj.transform.SetParent(_runner.ArtifactSpot);
        obj.transform.DOLocalMove(Vector3.zero, _gamePreset.ArtifactPickupSpeed);
        _artifactManager.SetPickedUpArtifact(obj);
    }
    
    private void ArtifactDrop()
    {
        if (_gameStateService.GameState != GameState.Play)
        {
            return;
        }

        _artifactManager.DropArtifact();
    }
}