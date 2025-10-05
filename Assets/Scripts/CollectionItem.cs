using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionItem : MonoBehaviour
{

    [SerializeField] private Image _itemImage;

    [SerializeField] private TextMeshProUGUI _itemCount;

    public void UpdateVisual(Sprite sprite)
    {
        _itemImage.sprite = sprite;
        
    }

    public void UpdateCount(int itemCount)
    {
        _itemCount.text = itemCount.ToString();
    }
}
