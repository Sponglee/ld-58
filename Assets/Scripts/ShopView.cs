using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : UIViewBase
{
    public event Action OnStartButtonPressed;
   
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _upgradeButton;


    private void Start()
    {
        _playButton.onClick.AddListener(StartPressed);
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(StartPressed);
    }

    private void StartPressed()
    {
        OnStartButtonPressed?.Invoke();
    }
}