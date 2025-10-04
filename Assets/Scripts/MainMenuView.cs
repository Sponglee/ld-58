using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenuView : UIViewBase
{
   public event Action OnStartButtonPressed;
   
   [SerializeField] private Button _playButton;
   [SerializeField] private Button _creditsButton;


   private void Start()
   {
      _playButton.onClick.AddListener(StartPressed);
   }

   private void OnDestroy()
   {
      _playButton.onClick.RemoveListener(StartPressed);
   }

   private void StartPressed()
   {
      OnStartButtonPressed?.Invoke();
   }
}
