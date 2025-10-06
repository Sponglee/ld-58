using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenuView : UIViewBase
{
   public event Action OnStartButtonPressed;
   public event Action OnCreditsButtonPressed;

   [SerializeField] private Button _playButton;
   [SerializeField] private Button _creditsButton;


   private void Start()
   {
      _playButton.onClick.AddListener(StartPressed);
      _creditsButton.onClick.AddListener(CreditsPressed);
   }

   private void OnDestroy()
   {
      _playButton.onClick.RemoveListener(StartPressed);
      _creditsButton.onClick.RemoveListener(CreditsPressed);
   }

   private void CreditsPressed()
   {
      OnCreditsButtonPressed?.Invoke();
   }

   private void StartPressed()
   {
      OnStartButtonPressed?.Invoke();
   }
}
