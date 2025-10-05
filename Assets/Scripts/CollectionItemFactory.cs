using UnityEngine;
using Zenject;

public class CollectionItemFactory 
{
    private readonly DiContainer _container;
    
    public CollectionItemFactory(DiContainer container)
    {
        _container = container;
    }
  
    public CollectionItem Create(GameObject prefab)
    {
        var collectionItem = _container.InstantiatePrefabForComponent<CollectionItem>(prefab);
        return collectionItem;
    }
}