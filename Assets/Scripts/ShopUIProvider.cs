using System;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class ShopUIProvider: IInitializable, IDisposable
{
    private GameStateService _gameStateService;
    private ShopController _shopController;
    private UpgradesManager _upgradesManager;

    
    public ShopUIProvider(
        GameStateService gameStateService,
        ShopView shopView,
        UpgradesManager upgradesManager)
    {
 
        _gameStateService = gameStateService;
        _upgradesManager = upgradesManager;

        
        var model = new ShopModel();

        _shopController = new ShopController(shopView, model);
        
    }

    public void Initialize()
    {
    
        
        _shopController.ToggleUI(true);
        
        _gameStateService.OnGameStateChanged += StateChangeHandler;
        _shopController.OnGameStartInput += StartGame;
        
        
        StateChangeHandler(_gameStateService.GameState);
    }
    
    public void Dispose()
    {
        _gameStateService.OnGameStateChanged -= StateChangeHandler;
        _shopController.OnGameStartInput -= StartGame;
    }

    private void StartGame()
    {
        PlayerPrefs.SetInt("SkipMenu",1);
        SceneManager.LoadScene("Main");
    }

    private void StateChangeHandler(GameState gameState)
    {
        var toggleState = gameState == GameState.Shop;
        _shopController.ToggleUI(toggleState);
    }


}