
    using System;
    using UnityEngine;
    using Zenject;

    public class MoneyManager : IInitializable, IDisposable
    {
        public event Action<int> OnMoneyChanged;
        
        private InventoryService _inventoryService; 
        private MoneyController _moneyController;
        
        public MoneyManager(
                InventoryService inventoryService,
                [Inject(Id = "MoneyView")] MoneyView view
            )
        {
            _inventoryService = inventoryService;
            
            var currentMoney = PlayerPrefs.GetInt("Money", 0);
            var moneyModel = new MoneyModel(currentMoney);
            var moneyView = view;
            
            _moneyController = new MoneyController(moneyModel, moneyView);
        }

        public void Initialize()
        {
            _inventoryService.OnItemStored += GetMoneyFromItem;
            
            _moneyController.Initialize();
        }

        public void Dispose()
        {
            _inventoryService.OnItemStored -= GetMoneyFromItem;
        }
        
        public void AddMoney(int amount)
        {
            _moneyController.AddMoney(amount);
        }

        public void SpendMoney(int amount)
        {
            _moneyController.TrySpendMoney(amount);
        }
        
        private void GetMoneyFromItem(InventoryItemData obj)
        {
            _moneyController.AddMoney(obj.MoneyValue);
        }

     
    }
