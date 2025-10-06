using DG.Tweening;
using UnityEngine;

public class HandView : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    public HandCell[] handCells; 
        
    private Ease _rotationEase;
    private float _rotationDuration;
    
    private void Awake()
    {
        SetData(null);
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
        if (data == null)
        {
            for (int i = 0; i < handCells.Length; i++)
            {
                handCells[i].ToggleCell(false);
            }
            return;
        }
    
        var lines = data.shape.Split('\n');
        int index = 0;
    
        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y].Trim();
            for (var x = 0; x < line.Length; x++)
            {
                var toggle = line[x] != '0';
                var cell = handCells[index];
                if (toggle)
                {
                    var image = data.cellList[int.Parse(line[x].ToString())-1];
                    cell.SetImage(image);
                }
                    
                cell.ToggleCell(toggle);
                  
                index++;
            }
        }

        // foreach (var cell in handCells)
        // {
        //     
        //     cell.SetImage(data.inventoryIcon);
        // }
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