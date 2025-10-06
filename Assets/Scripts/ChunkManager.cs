using System;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : IDisposable
{
    public delegate void ChunkCallback(WorldChunk chunk);

    private ChunkCallback _chunkCallback;
    
    private LinkedList<WorldChunk> _activeChunks = new LinkedList<WorldChunk>();

    public ChunkManager()
    {
        
    }
    
    public void Dispose()
    {
        _chunkCallback = null;
    }

    public void SetTileInitializedCallback(ChunkCallback callback)
    {
        _chunkCallback = callback;
    }
    public void AddTile(WorldChunk chunk)
    {
        _activeChunks.AddLast(chunk);
        _chunkCallback?.Invoke(chunk);
    }

    public void MoveChunks(float moveSpeed, WorldPreset preset)
    {
        var jumpTreshold = preset.JumpTreshold;

        if (_activeChunks.First.Value.Transform.position.z <= jumpTreshold) {
            var chunk = _activeChunks.First.Value;
            _activeChunks.RemoveFirst();
    
            var pos = _activeChunks.Last.Value.Transform.position.z + preset.TileSize;
            chunk.Transform.position = new Vector3(
                chunk.Transform.position.x,
                chunk.Transform.position.y,
                pos
            );

            AddTile(chunk);
        }
        
        foreach (var chunk in _activeChunks) {
            chunk.Move(moveSpeed * Time.deltaTime);
        }
        
    }

  
}