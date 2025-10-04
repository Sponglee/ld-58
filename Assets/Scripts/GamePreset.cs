using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "GamePreset", menuName = "Scriptable Objects/GamePreset")]
public class GamePreset : ScriptableObject
{
    public float LevelMoveSpeed = 10f;
    public float RunnerMoveSpeed = 10f;
[Header("--------Inventory----------")]
    public int InventoryCapacity = 5;
    public GameObject InventorySlotPrefab;

    [Header("--------ARTIFACTS----------")]
    public List<ArtifactPreset> ArtifactList;
    
}