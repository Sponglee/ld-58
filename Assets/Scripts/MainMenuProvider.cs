using System;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        var skipMenu = PlayerPrefs.GetInt("SkipMenu",0)==1;

        if (skipMenu)
        {
            StartGame();
        }
        
        var toggleState = gameState == GameState.Start;
        _mainMenuController.ToggleUI(toggleState);
    }


}