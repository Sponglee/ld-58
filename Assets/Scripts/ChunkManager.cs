using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;

public class ChunkManager
{
    private LinkedList<WorldChunk> _activeChunks = new LinkedList<WorldChunk>();

    public ChunkManager()
    {
        
    }
    
    public void AddTile(WorldChunk chunk)
    {
        _activeChunks.AddLast(chunk);
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