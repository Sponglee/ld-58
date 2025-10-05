    using UnityEngine;
    using Zenject;

    public class MoneyController : IInitializable
    {
        private MoneyView _moneyView;
        private MoneyModel _moneyModel;

        public MoneyController(MoneyModel model, MoneyView view)
        {
            _moneyView = view;
            _moneyModel = model;
        }
        public void Initialize()
        {
            _moneyView.UpdateMoneyText(_moneyModel.PlayerMoney.ToString());
        }
        
        public void ToggleUI(bool toggleState)
        {
            _moneyView.gameObject.SetActive(toggleState);
        }
        
        public void AddMoney(int addMoney)
        {
            var moneyAmount = _moneyModel.PlayerMoney;
            moneyAmount += addMoney;
            _moneyModel.PlayerMoney = moneyAmount;
            
            _moneyView.UpdateMoneyText(moneyAmount.ToString());
        }

        public bool TrySpendMoney(int removeMoney)
        {
            var moneyAmount = _moneyModel.PlayerMoney - removeMoney;

            if (removeMoney < 0)
            {
                return false;
            }
            
            _moneyModel.PlayerMoney = moneyAmount;
            _moneyView.UpdateMoneyText(moneyAmount.ToString());
            return true;
        }


        
    }
