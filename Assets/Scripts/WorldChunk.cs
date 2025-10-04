using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

public class WorldChunk : MonoBehaviour, IDisposable
{
    [SerializeField] private Transform[] _contentSpawnPoints;
    [SerializeField] private Transform _objectHolder;
    
    private Transform _t;
    private ChunkManager _chunkManager;

    public Transform Transform => _t ??= transform;
    public Transform ObjectHolder => _objectHolder;
    
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

    public Transform GetRandomSpawnPoint()
    {
        return _contentSpawnPoints[Random.Range(0, _contentSpawnPoints.Length)];
    }
}