using System;
using Zenject;

public class CollectionProvider: IInitializable, IDisposable
{
    private GameStateService _gameStateService;
    private GamePreset _gamePreset;
    private CollectionItemFactory _collectionItemFactory;
    private CollectionController _collectionController;
    private InventoryService _inventoryService;
    private CollectionService _collectionService;


    public CollectionProvider(
        GameStateService gameStateService,
        GamePreset gamePreset,
        CollectionView collectionView,
        InventoryService inventoryService,
        CollectionItemFactory collectionItemFactory,
        CollectionService collectionService,
        CollectionController collectionController,
        CollectionModel collectionModel)
    {
        _gameStateService = gameStateService;
        _gamePreset = gamePreset;
        _collectionController = collectionController;
        _collectionItemFactory = collectionItemFactory;
        _collectionService = collectionService;
        _inventoryService = inventoryService;
        
        _collectionController.Initialize(collectionView, collectionModel);
    }

    public void Initialize()
    {
        _gameStateService.OnGameStateChanged += StateChangeHandler;

        var artifacts = _gamePreset.ArtifactList;
        foreach (var artifact in artifacts)
        {
            var collectionItem = _collectionItemFactory.Create(_gamePreset.CollectionItemPrefab);
            _collectionService.RegisterCollection(collectionItem, artifact.inventoryData);
            collectionItem.UpdateVisual(artifact.inventoryData.inventoryIcon);
            collectionItem.UpdateCount(_collectionService.GetCollectedAmount(artifact.inventoryData.type));

            collectionItem.transform.SetParent(_collectionController.GetCollectionParent());
        }
        
        StateChangeHandler(_gameStateService.GameState);
        
        _inventoryService.OnItemStored += CollectedItemhandler;

    }

    private void CollectedItemhandler(InventoryItemData obj)
    {
        var collectionItem = _collectionService.GetCollectionItem(obj);
        _collectionService.ItemCollected(obj);
        var amount = _collectionService.GetCollectedAmount(obj.type);
        collectionItem.UpdateCount(amount);
    }

    public void Dispose()
    {
        _gameStateService.OnGameStateChanged -= StateChangeHandler;
        _inventoryService.OnItemStored -= CollectedItemhandler;
    }
    

    private void StateChangeHandler(GameState gameState)
    {
       
    }


}