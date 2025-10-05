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
        _gameStateService.OnGameStateChanged += StateChangeHandler;
        _mainMenuController.OnGameStartInput += StartGame;
        
        StateChangeHandler(_gameStateService.GameState);

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

    private void ExitGame()
    {
        
    }

    private void StateChangeHandler(GameState gameState)
    {
        if (gameState == GameState.Start)
        { 
            var skipMenu = PlayerPrefs.GetInt("SkipMenu",0)==1;

            if (skipMenu)
            {
                PlayerPrefs.SetInt("SkipMenu",0);
                StartGame();
                return;
            }
        }
       
        
        var toggleState = gameState == GameState.Start || gameState == GameState.Pause;
        _mainMenuController.ToggleUI(toggleState);
    }


}