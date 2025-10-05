using UnityEngine;
using Zenject;

public class HandFactory 
{
    private readonly DiContainer _container;
    
    public HandFactory(DiContainer container)
    {
        _container = container;
    }
  
    public HandView Create(GameObject prefab)
    {
        var hand = _container.InstantiatePrefabForComponent<HandView>(prefab);
        return hand;
    }
}