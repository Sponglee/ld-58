    using System;
    using System.Collections.Generic;
    using DefaultNamespace;
    using DG.Tweening;
    using UnityEngine;
    using Zenject;

    public class HandService: ITickable
    {
        public event Action OnHandDropped;

        
        private bool _isHandEmpty = true;
            
        public List<HandController> _storedHands = new List<HandController>();
        
        private  HandController _activeHand;
        private ItemMouseService _mouseService;
        private GamePreset _gamePreset;
        private HandFactory _handFactory;
        private GameUIController _gameUIController;
        
        public bool IsHandEmpty => _activeHand == null;
        public HandController ActiveHand => _activeHand;

        public HandService(
            ItemMouseService mouseService,
            HandFactory factory,
            GameUIController gameUIController,
            GamePreset gamePreset)
        {
            _mouseService = mouseService;
            _gamePreset = gamePreset;
            _handFactory = factory;
            _gameUIController = gameUIController;
        }
        
        public void Tick()
        {
            if (IsHandEmpty)
            {
                return;
            }

            _activeHand?.SetHandPosition(_mouseService.MousePosition);
        }

        public void RotateActiveHand(bool isClockwise)
        {
            _activeHand?.RotateHand(isClockwise);
        }

        public void GrabOrCreateHand(InventoryItemData inventoryData)
        {
            var viewPrefab = _gamePreset.HandPrefab;
            
            var handModel = new HandModel();
            var handView = _handFactory.Create(viewPrefab);


            var handParent = _gameUIController.GetHandParent();
            handView.transform.SetParent(handParent);
            handView.transform.localScale = Vector3.one;
            
            var controller = new HandController(handModel, handView);
            controller.InitializeHand(inventoryData, _gamePreset.HandRotateDuration, _gamePreset.HandRotateEase);
            _activeHand = controller;
        }
        
        public void DiscardHand()
        {
            var hand = _activeHand;
            _activeHand = null;
            hand?.DeactivateHand();
            hand?.Dispose();
            OnHandDropped?.Invoke();
        }


        public void StoreHand(HandController hand, InventorySlotController slot)
        {
           _activeHand = null;
           _storedHands.Add(hand);
           hand.Transform.DOMove(slot.Transform.position, 0.25f).SetEase(Ease.OutCubic);
           OnHandDropped?.Invoke();
        }
    }
