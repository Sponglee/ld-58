using UnityEngine;
using UnityEngine.UI;

public class InventorySlotView : MonoBehaviour
{
    [SerializeField] private Image _icon;


    public void SetImage(Sprite sprite)
    {
        _icon.sprite = sprite;
    }
    
}