using UnityEngine;
using UnityEngine.UI;

public class HandCell : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image cellImage;
    
    public void ToggleCell(bool toggle)
    {
        _canvasGroup.alpha = toggle ? 1 : 0;
    }

    public void SetImage(Sprite sprite)
    {
        cellImage.sprite = sprite;
    }
}