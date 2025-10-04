using System.Collections.Generic;
using UnityEngine;

public class ChunkManager
{
    
    
    private List<WorldChunk> _activeChunks = new List<WorldChunk>();
    
    public ChunkManager()
    {
        
    }
    
    public void AddTile(WorldChunk chunk)
    {
        _activeChunks.Add(chunk);
    }

    public void MoveChunks(float moveSpeed)
    {
        foreach (var chunk in _activeChunks)
        {
            chunk.Move(moveSpeed*Time.deltaTime);
        }
    }
}