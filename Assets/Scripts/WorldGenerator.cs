using DefaultNamespace;
using UnityEngine;
using Zenject;

public class WorldGenerator : IInitializable
{
    private IChunkFactory _chunkFactory;
    private WorldPreset _worldPreset;
    private Transform _worldHolder;
    private ChunkManager _chunkManager;

    [Inject]
    public void Construct(
        IChunkFactory chunkFactory, 
        WorldPreset worldPreset, 
        ChunkManager chunkManager,
        [Inject(Id = "WorldSpawn")] Transform worldHolder)
    {
        _chunkFactory = chunkFactory;
        _worldPreset = worldPreset;
        _worldHolder = worldHolder;
        _chunkManager = chunkManager;
    }

    public void Initialize()
    {
        GenerateLevel();
        GenerateObjects();
    }

    public void GenerateObjects()
    {
        
    }
    
    public void GenerateLevel()
    {
        var levelSize = _worldPreset.MapSize;

        for (int y = 0; y < levelSize; y++)
        {
            var hasTile = GetTilePrefab(y, out var tilePrefab);
            if (!hasTile)
                continue;

            Vector3 pos = GetSpawnPosition(y, _worldPreset.TileSize);
            var tile = _chunkFactory.Create(tilePrefab, pos);
            tile.Transform.SetParent(_worldHolder);
            _chunkManager.AddTile(tile);
        }
    }

    private Vector3 GetSpawnPosition(int tileY, float tileSize)
    {
        var position = new Vector3(0f,0f, tileY * tileSize);

        return position;
    }

    private bool GetTilePrefab(int y, out GameObject tilePrefab)
    {
        tilePrefab = null;
        var map = _worldPreset.TileList;

        tilePrefab = map[Random.Range(0, map.Length)];

        return tilePrefab != null;
    }
}