using UnityEngine;

public class HandCell : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    
    public void ToggleCell(bool toggle)
    {
        _canvasGroup.alpha = toggle ? 1 : 0;
    }
}