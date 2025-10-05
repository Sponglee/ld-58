using System;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotView : MonoBehaviour
{
    public event Action OnSlotButtonPressed;
    
    [SerializeField] private Image _icon;

    [SerializeField] private Button _cellButton;

    private void Awake()
    {
        _cellButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnDestroy()
    {
        _cellButton.onClick.RemoveListener(OnButtonClicked);
    }

    public void SetImage(Sprite sprite)
    {
        _icon.sprite = sprite;
    }
    
    
    private void OnButtonClicked()
    {
        OnSlotButtonPressed?.Invoke();
    }
}