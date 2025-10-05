
    using System;
    using Zenject;

    public class MoneyManager : IDisposable
    {
        public event Action<int> OnMoneyChanged;
        
        private InventoryService _inventoryService; 
        private MoneyController _moneyController;
        
        public MoneyManager(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public void Initialize(MoneyController moneyController)
        {
            _moneyController = moneyController;
        }

        public void Dispose()
        {
        }

        public void AddMoney(int amount)
        {
            _moneyController.AddMoney(amount);
        }

        public bool SpendMoney(int amount)
        {
            return _moneyController.TrySpendMoney(amount);
        }
        
      

     
    }
