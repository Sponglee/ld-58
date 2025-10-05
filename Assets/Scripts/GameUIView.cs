using System;
using UnityEngine;
using UnityEngine.UI;

public class GameUIView : UIViewBase
{
   public event Action OnDayCompleteButtonPressed;
   public event Action OnPauseButtonPressed;
   [SerializeField] private RectTransform _rectTransform;
   [SerializeField] private Button _pauseButton;
   [SerializeField] private Button _endDayButton;
   [SerializeField] private Transform _inventoryHolder;
   [SerializeField] private Transform _artifactHolder;
   [SerializeField] private Transform _handHolder;
   [SerializeField] private Transform pointerHolder;
   
   private void Start()
   {
      _pauseButton.onClick.AddListener(PausePressed);
      _endDayButton.onClick.AddListener(DayCompletePressed);
   }

   private void OnDestroy()
   {
      _pauseButton.onClick.RemoveListener(PausePressed);
      _endDayButton.onClick.RemoveListener(DayCompletePressed);
   }
   
   private void PausePressed()
   {
      OnPauseButtonPressed?.Invoke();
   }
   
   private void DayCompletePressed()
   {
      OnDayCompleteButtonPressed?.Invoke();
   }

   public Transform GetInventoryParent()
   {
      return _inventoryHolder;
   }
   
   public Transform GetArtifactParent()
   {
      return _artifactHolder;
   }

   public Transform GetHandParent()
   {
      return _handHolder;
   }
   
   public RectTransform GetRectTransform()
   {
      return _rectTransform;
   }
}