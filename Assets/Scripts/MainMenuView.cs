using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : UIViewBase
{
   public event Action OnStartButtonPressed;
   
   [SerializeField] private Button playButton;
   [SerializeField] private Button creditsButton;


   private void Start()
   {
      playButton.onClick.AddListener(StartPressed);
   }

   private void OnDestroy()
   {
      playButton.onClick.RemoveListener(StartPressed);
   }

   private void StartPressed()
   {
      OnStartButtonPressed?.Invoke();
   }
}
