using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameUIView : UIViewBase
{
   public event Action OnPauseButtonPressed;
   [SerializeField] private RectTransform _rectTransform;
   [SerializeField] private HandView _handView;
   [SerializeField] private Button _pauseButton;
   [SerializeField] private Transform _inventoryHolder;
   [SerializeField] private Transform _artifactHolder;
   
   private void Start()
   {
      _pauseButton.onClick.AddListener(PausePressed);
   }

   private void OnDestroy()
   {
      _pauseButton.onClick.RemoveListener(PausePressed);
   }
   
   private void PausePressed()
   {
      OnPauseButtonPressed?.Invoke();
   }

   public Transform GetInventoryParent()
   {
      return _inventoryHolder;
   }

   public RectTransform GetRectTransform()
   {
      return _rectTransform;
   }

   public void InitializeHand(InventoryItemData data, float rotationDuration, Ease ease)
   {
      _handView.SetData(data);
      _handView.SetSettings(rotationDuration, ease);
      _handView.ToggleHand(true);
   }
   public void SetHandPosition(Vector3 position)
   {
      _handView.SetPosition(position);
   }

   public void RotateHand(bool isClockwise)
   {
      _handView.RotateAround(isClockwise);
   }

   public void DeinitializeHand()
   {
      _handView.SetData(null);
      _handView.ToggleHand(false);
   }
}