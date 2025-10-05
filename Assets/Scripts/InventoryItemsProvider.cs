    using System;
    using System.Collections.Generic;
    using UnityEditor;
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
        private HandService _handService;
        private GamePreset _gamePreset;

        
        public InventoryItemsProvider(
            InventorySlotViewFactory inventorySlotViewFactory,
            InventoryService inventoryService,
            GameUIController gameUIController,
            ArtifactProvider artifactProvider,
            ArtifactManager artifactManager,
            ItemMouseService itemMouseService,
            HandService handService,
            GamePreset gamePreset)
        {
            _viewFactory = inventorySlotViewFactory;
            _inventoryService = inventoryService;
            _gameUIController = gameUIController;
            _gamePreset = gamePreset;
            _artifactProvider = artifactProvider;
            _artifactManager = artifactManager;
            _itemMouseService = itemMouseService;
            _handService = handService;
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
            if (_handService.IsHandEmpty)
            {
                return;
            }
            
            var artifact = _artifactManager.PickedUpArtifact;
            var slotsToFill = new List<InventorySlotController>();
            var artifactData = _artifactManager.GetDataByArtifact(artifact);
            var shapeString = CalculateHandRotatedShape(artifactData);
            var canFit = _inventoryService.CheckSlot(obj, artifactData, shapeString, out slotsToFill);
            var hand = _handService.ActiveHand;
            
            if (canFit)
            {
                foreach (var slot in slotsToFill)
                {
                    slot.FillSlot(artifactData);
                }
                
                _handService.StoreHand(hand, obj);
                _handService.DiscardHand();
                _inventoryService.ItemStored(artifactData);
            }
        }

        public void Dispose()
        {
            _inventoryService.OnCellClicked -= CellClicked;
            _artifactProvider.OnArtifactPickedUp -= ArtifactPickedUp;
            _itemMouseService.OnHandCanceled -= HandEmptied;
            _itemMouseService.OnScroll -= ScrollAction;
        }
        
        private void StoreArtifact(HandController hand, Artifact obj)
        {
            
        }
        
        private void ArtifactPickedUp(Artifact obj)
        {
            var inventoryData = _artifactManager.GetDataByArtifact(obj);
            _handService.GrabOrCreateHand(inventoryData);
        }

        private void HandEmptied()
        {
            _handService.DiscardHand();
        }

        private void ScrollAction()
        {
            var isClockwise = _itemMouseService.CameraScrollInput>=0;
            _handService.RotateActiveHand(isClockwise);
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
        
        private string CalculateHandRotatedShape(InventoryItemData artifactData)
        {
           var handRotation= _handService.ActiveHand.GetRotationAngle();
            var shapeToRotate = artifactData.shape;
            var resultShape = shapeToRotate;
            var turnCount = handRotation / 90;
            
            switch (handRotation)
            {
                case < 0:
                {
                    for (var i = 0; i < turnCount; i++)
                    {
                        resultShape = artifactData.RotateShape(resultShape);
                    }

                    break;
                }
                case > 0:
                {
                    for (var i = 0; i < turnCount; i++)
                    {
                        resultShape = artifactData.RotateShapeCounterClockwise(resultShape);
                    }

                    break;
                }
            }

            return resultShape;
        }

    }