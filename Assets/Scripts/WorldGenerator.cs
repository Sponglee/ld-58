using UnityEngine;
using Zenject;

public class WorldGenerator : IInitializable
{
    private IChunkFactory _chunkFactory;
    private IArtifactFactory _artifactFactory;

    private WorldPreset _worldPreset;
    private GamePreset _gamePreset;
    private Transform _worldHolder;
    private ChunkManager _chunkManager;
    private ArtifactManager _artifactManager;

    [Inject]
    public void Construct(
        IChunkFactory chunkFactory, 
        IArtifactFactory artifactFactory,
        WorldPreset worldPreset, 
        GamePreset gamePreset,
        ChunkManager chunkManager,
        ArtifactManager artifactManager,
        [Inject(Id = "WorldSpawn")] Transform worldHolder)
    {
        _chunkFactory = chunkFactory;
        _artifactFactory = artifactFactory;
        _worldPreset = worldPreset;
        _gamePreset = gamePreset;
        _worldHolder = worldHolder;
        _chunkManager = chunkManager;
        _artifactManager = artifactManager;
    }

    public void Initialize()
    {
        _chunkManager.SetTileInitializedCallback(GenerateObjects);
        GenerateLevel();
    }

    public void GenerateObjects(WorldChunk chunk)
    {
        var hasArtifacts = Random.Range(0, 100) <= _worldPreset.ArtifactSpawnProbability;
        if (!hasArtifacts)
        {
            return;
        }

        TryGetArtifactPrefab(out var artifactPrefab, out var inventoryItemData);
        
        var spawnPoint = chunk.GetRandomSpawnPoint();
        var chunkObjectHolder = chunk.ObjectHolder;
        
        var artifact = _artifactFactory.Create(artifactPrefab, spawnPoint.position);
        artifact.GetTransform().SetParent(chunkObjectHolder);
        _artifactManager.AddArtifact(inventoryItemData, artifact);
    }
    
    public void GenerateLevel()
    {
        var levelSize = _worldPreset.MapSize;

        for (int y = 0; y < levelSize; y++)
        {
            var hasTile = TryGetTilePrefab(y, out var tilePrefab);
            if (!hasTile)
            {
                continue;
            }

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

    private bool TryGetTilePrefab(int y, out GameObject tilePrefab)
    {
        tilePrefab = null;
        var map = _worldPreset.TileList;

        tilePrefab = map[Random.Range(0, map.Length)];

        return tilePrefab != null;
    }

    private bool TryGetArtifactPrefab(out GameObject artifactPrefab, out InventoryItemData data)
    {
        artifactPrefab = null;
        data = null;
        var artifactList = _gamePreset.ArtifactList;
        var preset =artifactList[Random.Range(0, artifactList.Count)];

        artifactPrefab = preset.artifactPrefab;
        data = preset.inventoryData;
        
        return artifactPrefab == null;
    }
}