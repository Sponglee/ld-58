    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class UpgradesManager
    {
        private List<UpgradeData> _upgrades = new List<UpgradeData>();
        
        public UpgradesManager()
        {
            
        }


        public UpgradeData GetUpgradeDataByType(UpgradeType type)
        {
            foreach (var upgrade in _upgrades)
            {
                if (upgrade.UpgradeType == type)
                {
                    return upgrade;
                }
            }

            return null;
        }

        public void UpgradeItem(UpgradeType upgradeType)
        {
            foreach (var upgrade in _upgrades)
            {
                if (upgrade.UpgradeType == upgradeType)
                {
                    var level = upgrade.UpgradeLevel;
                    level++;
                    PlayerPrefs.SetInt(upgrade.UpgradeKey, level);
                    upgrade.UpgradeLevel = level;
                }
            }
        }

        public void Initialize(UpgradePreset upgradePreset)
        {
            foreach (var pair in upgradePreset.Upgrades)
            {
                var data = new UpgradeData();
                data.UpgradeType = pair.UpgradeData.UpgradeType;
                data.UpgradeCost = pair.UpgradeData.UpgradeCost;
                data.UpgradeKey = pair.Key;
                data.UpgradeLevel = PlayerPrefs.GetInt(pair.Key,0);
                _upgrades.Add(data);
            }
        }
    }
    
    [Serializable]
    public class UpgradeData
    {
        public UpgradeType UpgradeType;
        public int UpgradeCost;
        public int UpgradeLevel;
        public string UpgradeKey;
        public int CalculatedCost;

        public UpgradeData()
        {
            
        }

    }

    public enum UpgradeType
    {
        Capacity
    }
