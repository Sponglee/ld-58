using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class WorldChunkFactory : IChunkFactory
    {
        private readonly DiContainer _container;
    
        public WorldChunkFactory(DiContainer container)
        {
            _container = container;
        }
  
        public WorldChunk Create(GameObject prefab, Vector3 position)
        {
            var tile = _container.InstantiatePrefabForComponent<WorldChunk>(prefab);
            tile.transform.position = position;
            return tile;
        }
    }
}