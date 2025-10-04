using System;
using UnityEngine;
using UnityEngine.UI;

public class GameUIView : UIViewBase
{
   public event Action OnPauseButtonPressed;
   
   [SerializeField] private Button _pauseButton;
   
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
}