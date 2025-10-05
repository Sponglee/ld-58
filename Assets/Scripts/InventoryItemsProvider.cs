    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using UnityEngine;
    using Zenject;

    public class InventoryItemsProvider: IInitializable, IDisposable
    {
        private InventorySlotViewFactory _viewFactory;
        private InventoryService _inventoryService;
        private GameUIController _gameUIController;
        private ArtifactProvider _artifactProvider;
        private ArtifactManager _artifactManager;
        private ItemMouseService _itemMouseService;
        // private HandService _handService;
        private GamePreset _gamePreset;

        
        public InventoryItemsProvider(
            InventorySlotViewFactory inventorySlotViewFactory,
            InventoryService inventoryService,
            GameUIController gameUIController,
            ArtifactProvider artifactProvider,
            ArtifactManager artifactManager,
            ItemMouseService itemMouseService,
            // HandService handService,
            GamePreset gamePreset)
        {
            _viewFactory = inventorySlotViewFactory;
            _inventoryService = inventoryService;
            _gameUIController = gameUIController;
            _gamePreset = gamePreset;
            _artifactProvider = artifactProvider;
            _artifactManager = artifactManager;
            _itemMouseService = itemMouseService;
            // _handService = handService;
        }


        public void Initialize()
        {
            InitializeInventorySlots();
            _inventoryService.OnCellClicked += CellClicked;
            _artifactProvider.OnArtifactPickedUp += ArtifactPickedUp;
            _itemMouseService.OnHandCanceled += HandEmptied;
            _itemMouseService.OnScroll += ScrollAction;
        }

        private void CellClicked(InventorySlotController obj)
        {
            var artifact = _artifactManager.PickedUpArtifact;
            var slotsToFill = new List<InventorySlotController>();
            var artifactData = _artifactManager.GetDataByArtifact(artifact);
            var canFit = _inventoryService.CheckSlot(obj, artifactData, out slotsToFill);

            if (canFit)
            {
                foreach (var slot in slotsToFill)
                {
                    slot.FillSlot(artifactData);
                }
                
                HandEmptied();
                StoreArtifact(obj, artifact);
            }
        }

        public void Dispose()
        {
            _inventoryService.OnCellClicked -= CellClicked;
            _artifactProvider.OnArtifactPickedUp -= ArtifactPickedUp;
            _itemMouseService.OnHandCanceled -= HandEmptied;
            _itemMouseService.OnScroll -= ScrollAction;
        }
        
        private void StoreArtifact(InventorySlotController inventorySlotController, Artifact obj)
        {

        }
        
        private void ArtifactPickedUp(Artifact obj)
        {
            var inventoryData = _artifactManager.GetDataByArtifact(obj);
            _itemMouseService.GrabHand(inventoryData, _gamePreset.HandRotateDuration, _gamePreset.HandRotateEase);
        }

        private void HandEmptied()
        {
            _itemMouseService.EmptyHand();
        }

        private void ScrollAction()
        {
            var isClockwise = _itemMouseService.CameraScrollInput>=0;
            _gameUIController.RotateHand(isClockwise);
        }
        
        private void InitializeInventorySlots()
        {
            var slots = new List<InventorySlotController>();
            
            var columns = _gamePreset.InventoryDimentions.x;
            var rows = _gamePreset.InventoryDimentions.y;
            var x = 0;
            var y = 0;
            
            for (var i = 0; i < _gamePreset.InventoryCapacity; i++)
            {
                var slotModel = new InventorySlotModel(new UnityEngine.Vector2(x,y), true);
                Debug.Log(x +" : " +y);
                var slotView = _viewFactory.Create(_gamePreset.InventorySlotPrefab, _gameUIController.GetInventoryParent());
                var slot = new InventorySlotController(slotModel, slotView);
                slot.Initialize();
                slots.Add(slot);
                x++;
                if (x >= columns)
                {
                    x = 0;
                    y++;
                }
            }
          
            _inventoryService.SetUpSlots(slots);
        }

    }