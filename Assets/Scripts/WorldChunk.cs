using System;
using UnityEngine;
using Zenject;

public class WorldChunk : MonoBehaviour, IInitializable, IDisposable
{
    public Transform Transform => transform;

    private ChunkManager _chunkManager;

    [Inject]
    public void Construct(ChunkManager chunkManager)
    {
        _chunkManager = chunkManager;
    }

    public void Initialize()
    {

    }
    
    public void Dispose()
    {

    }

    public void Move(float moveOffset)
    {
        transform.Translate(Vector3.back * moveOffset);
    }
}