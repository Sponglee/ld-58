using UnityEngine;

namespace DefaultNamespace
{
    public interface IChunkFactory
    {
        WorldChunk Create(GameObject prefab, Vector3 position);
    }
}