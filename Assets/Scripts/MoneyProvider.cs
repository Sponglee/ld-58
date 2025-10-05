using System;
using UnityEngine;
using Zenject;

public class MoneyProvider: IInitializable, IDisposable
{
    private GameStateService _gameStateService;
    private MoneyController _moneyController;
    private UpgradesManager _upgradesManager;
    private InventoryService _inventoryService;
    private MoneyManager _moneyManager;
    private UpgradePreset _upgradePreset;
    
    public MoneyProvider(
        GameStateService gameStateService,
        InventoryService inventoryService,
        MoneyView moneyView,
        UpgradePreset upgradePreset,
        MoneyManager moneyManager,
        UpgradesManager upgradesManager)
    {

        _inventoryService = inventoryService;
        _gameStateService = gameStateService;
        _upgradesManager = upgradesManager;
        _upgradePreset = upgradePreset;
        _moneyManager = moneyManager;
        
        var currentMoney = PlayerPrefs.GetInt("Money", 0);
        var moneyModel = new MoneyModel(currentMoney);
            
        _moneyController = new MoneyController(moneyModel, moneyView);
        
        _inventoryService.OnItemStored += GetMoneyFromItem;
        _gameStateService.OnGameStateChanged += StateChangeHandler;

        _moneyController.Initialize();
        _moneyManager.Initialize(_moneyController);
        StateChangeHandler(_gameStateService.GameState);
        _upgradesManager.Initialize(_upgradePreset);

    }

    public void Initialize()
    {
      

    }
    
    public void Dispose()
    {
        _inventoryService.OnItemStored -= GetMoneyFromItem;
        _gameStateService.OnGameStateChanged -= StateChangeHandler;
        

    }

    private void GetMoneyFromItem(InventoryItemData obj)
    {
        _moneyManager.AddMoney(obj.MoneyValue);
    }

    private void StateChangeHandler(GameState gameState)
    {
        var toggleState = gameState != GameState.Start;
        _moneyController.ToggleUI(toggleState);
    }


}