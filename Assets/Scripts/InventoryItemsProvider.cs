    using System;
    using System.Collections.Generic;
    using Zenject;

    public class InventoryItemsProvider: IInitializable, IDisposable
    {
        private InventorySlotViewFactory _viewFactory;
        private InventoryService _inventoryService;
        private GameUIController _gameUIController;
        private ArtifactProvider _artifactProvider;
        private ArtifactManager _artifactManager;
        private ItemMouseService _itemMouseService;
        private GamePreset _gamePreset;

        
        public InventoryItemsProvider(
            InventorySlotViewFactory inventorySlotViewFactory,
            InventoryService inventoryService,
            GameUIController gameUIController,
            ArtifactProvider artifactProvider,
            ArtifactManager artifactManager,
            ItemMouseService itemMouseService,
            GamePreset gamePreset)
        {
            _viewFactory = inventorySlotViewFactory;
            _inventoryService = inventoryService;
            _gameUIController = gameUIController;
            _gamePreset = gamePreset;
            _artifactProvider = artifactProvider;
            _artifactManager = artifactManager;
            _itemMouseService = itemMouseService;
        }


        public void Initialize()
        {
            InitializeInventorySlots();

            _artifactProvider.OnArtifactPickedUp += ArtifactPickedUp;
            _itemMouseService.OnHandCanceled += ArtifactDropped;
            _itemMouseService.OnScroll += ScrollAction;
        }

        public void Dispose()
        {
            _artifactProvider.OnArtifactPickedUp -= ArtifactPickedUp;
            _itemMouseService.OnHandCanceled -= ArtifactDropped;
            _itemMouseService.OnScroll -= ScrollAction;
        }
        
        private void ArtifactPickedUp(Artifact obj)
        {
            var inventoryData = _artifactManager.GetDataByArtifact(obj);
            _itemMouseService.GrabHand(inventoryData, _gamePreset.HandRotateDuration, _gamePreset.HandRotateEase);
            _artifactManager.DestroyArtifact(obj);
        }

        private void ArtifactDropped()
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
            for (var i = 0; i < _gamePreset.InventoryCapacity; i++)
            {
                var slotModel = new InventorySlotModel();
                var slotView = _viewFactory.Create(_gamePreset.InventorySlotPrefab, _gameUIController.GetInventoryParent());
                var slot = new InventorySlotController(slotModel, slotView);
                slot.Initialize();
                slots.Add(slot);
            }
          
            _inventoryService.SetUpSlots(slots);
        }

    
    }