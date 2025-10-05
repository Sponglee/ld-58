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
    
    public MoneyProvider(
        GameStateService gameStateService,
        InventoryService inventoryService,
        MoneyView moneyView,
        MoneyManager moneyManager,
        UpgradesManager upgradesManager)
    {

        _inventoryService = inventoryService;
        _gameStateService = gameStateService;
        _upgradesManager = upgradesManager;
        _moneyManager = moneyManager;
        
        var currentMoney = PlayerPrefs.GetInt("Money", 0);
        var moneyModel = new MoneyModel(currentMoney);
            
        _moneyController = new MoneyController(moneyModel, moneyView);
    }

    public void Initialize()
    {
        _inventoryService.OnItemStored += GetMoneyFromItem;
        _gameStateService.OnGameStateChanged += StateChangeHandler;

        _moneyController.Initialize();
        _moneyManager.Initialize(_moneyController);

        StateChangeHandler(_gameStateService.GameState);

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