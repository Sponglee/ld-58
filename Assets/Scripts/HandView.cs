using DG.Tweening;
using UnityEngine;

public class HandView : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    private Ease _rotationEase;
    private float _rotationDuration;
    
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void SetSettings(float rotationDuration, Ease rotationEase)
    {
        _rotationEase = rotationEase;
        _rotationDuration = rotationDuration;
    }

    public void ToggleHand(bool toggle)
    {
        gameObject.SetActive(toggle);
    }
    public void SetPosition(Vector3 position)
    {
         _rectTransform.anchoredPosition = position;
    }

    public void SetData(InventoryItemData data)
    {
        
    }

    public void RotateAround(bool isClockwise)
    {
        if (DOTween.IsTweening(transform)) return;
        // transform.DOKill();
        float currentZ = transform.eulerAngles.z;
        float snapped = Mathf.Round(currentZ / 90f) * 90f;
        float target = snapped + (isClockwise ? -90 : 90);
        transform.DORotate(new Vector3(0, 0, target), _rotationDuration)
            .SetEase(_rotationEase);
    }
}