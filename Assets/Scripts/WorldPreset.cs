using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "WorldPreset", menuName = "Scriptable Objects/WorldPreset")]
public class WorldPreset : ScriptableObject
{
    public float TileSize;
    public GameObject[] TileList;
    public int MapSize;
    [Range(0,100)]
    public int ArtifactSpawnProbability;
    public int JumpTreshold;
    // [TextArea(20,20)]
    // public string Map;
    // [TextArea(20,20)]
    // public string MapContent;

}