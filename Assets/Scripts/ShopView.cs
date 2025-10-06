using System;
using Mono.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : UIViewBase
{
    public event Action OnStartButtonPressed;
    public event Action<UpgradeType> OnUpgradePressed;

    [SerializeField] private Button _playButton;
    [SerializeField] private UpgradeItem _upgradeItem;
    [SerializeField] private CollectionView _collection;
    
    private void Start()
    {
        _playButton.onClick.AddListener(StartPressed);
        _upgradeItem.Button.onClick.AddListener(UpgradePressed);
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(StartPressed);
        _upgradeItem.Button.onClick.RemoveListener(UpgradePressed);
    }
    
    public void UpdateUpgrade(UpgradeData data)
    {
        var upgradeLevel = data.UpgradeLevel;
        var upgradePrice = data.CalculatedCost;
        
        _upgradeItem.UpdateVisual(upgradePrice, upgradeLevel);
    }
    
    private void UpgradePressed()
    {
        OnUpgradePressed?.Invoke(_upgradeItem.UpgradeType);
    }
    
    private void StartPressed()
    {
        OnStartButtonPressed?.Invoke();
    }

    
}