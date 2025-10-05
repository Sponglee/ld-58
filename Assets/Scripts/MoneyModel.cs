    using UnityEngine;

    public class MoneyModel
    {
        private int _playerMoney;

        public int PlayerMoney
        {
            get
            {
                var money = PlayerPrefs.GetInt("Money", 0);
                _playerMoney = money;
                return _playerMoney;
            }
            set
            {
                _playerMoney = value;
                PlayerPrefs.SetInt("Money", _playerMoney);
            }
        }


        public MoneyModel(int currentMoney)
        {
            _playerMoney = currentMoney;
        }
    }
