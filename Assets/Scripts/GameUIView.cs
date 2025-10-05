using System;
using DefaultNamespace;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameUIView : UIViewBase
{
   public event Action OnPauseButtonPressed;
   [SerializeField] private RectTransform _rectTransform;
   [SerializeField] private Button _pauseButton;
   [SerializeField] private Transform _inventoryHolder;
   [SerializeField] private Transform _artifactHolder;
   [SerializeField] private Transform _handHolder;
   [SerializeField] private Transform pointerHolder;
   
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