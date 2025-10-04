using UnityEngine;

public class HandView : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    private void Awake()
    {
        gameObject.SetActive(false);
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
}