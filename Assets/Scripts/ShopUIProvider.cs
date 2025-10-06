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
    private MoneyManager _moneyManager;

    
    public ShopUIProvider(
        GameStateService gameStateService,
        ShopView shopView,
        UpgradesManager upgradesManager,
        MoneyManager moneyManager)
    {
        _moneyManager = moneyManager;
        _gameStateService = gameStateService;
        _upgradesManager = upgradesManager;

        
        var model = new ShopModel();

        _shopController = new ShopController(shopView, model);
        
    }

    public void Initialize()
    {
    
        _shopController.Initialize();
        
        _shopController.ToggleUI(true);

        var upgradeData = _upgradesManager.GetUpgradeDataByType(UpgradeType.Capacity);
        var resultCost = GetResultCost(upgradeData);
        upgradeData.CalculatedCost = resultCost;
        
        _shopController.UpgradeVisual(upgradeData);

        
        _gameStateService.OnGameStateChanged += StateChangeHandler;
        _shopController.OnGameStartInput += StartGame;
        _shopController.OnUpgradeInput += UpgradeHandler;
        
        StateChangeHandler(_gameStateService.GameState);
    }
    
    public void Dispose()
    {
        _gameStateService.OnGameStateChanged -= StateChangeHandler;
        _shopController.OnGameStartInput -= StartGame;
        _shopController.OnUpgradeInput += UpgradeHandler;
    }

    private void UpgradeHandler(UpgradeType obj)
    {
        var upgradeData = _upgradesManager.GetUpgradeDataByType(obj);
     
        var resultCost = GetResultCost(upgradeData);
        upgradeData.CalculatedCost = resultCost;
        
        var isSpent = _moneyManager.SpendMoney(resultCost);
        if (!isSpent)
        {
            return;
        }
        
        _upgradesManager.UpgradeItem(obj);
        
        resultCost = GetResultCost(upgradeData);
        upgradeData.CalculatedCost = resultCost;
        
        _shopController.UpgradeVisual(upgradeData);
    }

    private int GetResultCost(UpgradeData upgradeData)
    {
        var upgradeCost = upgradeData.UpgradeCost * (1 + Math.Log(upgradeData.UpgradeLevel + 1));
        var resultCost = (int)(Math.Ceiling(upgradeCost / 100) * 100);
        return resultCost;
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