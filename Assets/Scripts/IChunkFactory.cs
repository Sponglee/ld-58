using UnityEngine;


    public interface IChunkFactory
    {
        WorldChunk Create(GameObject prefab, Vector3 position);
    }
