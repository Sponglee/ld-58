using System;
using UnityEngine;
using Zenject;

public class RunnerProvider: IInitializable, IDisposable, ITickable
{
    private GamePreset _gamePreset;
    private GameStateService _gameStateService;
    private RunnerInputService _runnerInputService;
    private RunnerView _runnerView;
    
    
    private float _stopTimer = 0;

    
    public RunnerProvider(
        RunnerInputService runnerInputService, 
        GameStateService gameStateService,
        RunnerView runnerView,
        GamePreset gamePreset)
    {
        _gameStateService = gameStateService;
        _runnerInputService = runnerInputService;
        _runnerView = runnerView;
        _gamePreset = gamePreset;
    }

    public void Initialize()
    {
        _gameStateService.OnGameStateChanged += StateChangeHandler;
        _runnerInputService.OnRunnerMove += MoveRunner;
        _runnerInputService.OnRunnerStay += StopRunner;

    }



    public void Dispose()
    {
        _gameStateService.OnGameStateChanged -= StateChangeHandler;
        _runnerInputService.OnRunnerMove -= MoveRunner;
        _runnerInputService.OnRunnerStay -= StopRunner;

    }

    private void MoveRunner(Vector2 obj)
    {
        _stopTimer = 0;
        var input = _runnerInputService.RunnerMoveInput;   
        var moveVector = new Vector3(input.x, 0f,0f) * _gamePreset.RunnerMoveSpeed * Time.deltaTime;
        _runnerView.MoveRunner(moveVector);
    }

    private void StopRunner(Vector2 obj)
    {
        _stopTimer += Time.deltaTime;
        
        var input = _runnerInputService.RunnerMoveInput;   
        var moveVector = new Vector3(input.x, 0f,0f) * _gamePreset.RunnerMoveSpeed * (1-_stopTimer/_gamePreset.RunnerStopTime) * Time.deltaTime;
        
        _runnerView.MoveRunner(moveVector);
    }

    public void Tick()
    {

    }
    
    private void StateChangeHandler(GameState gameState)
    {
        var toggleState = gameState == GameState.Play;
    }
}