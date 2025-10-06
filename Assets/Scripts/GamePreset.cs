using System.Collections.Generic;
using DG.Tweening;
using Unity.Burst.Intrinsics;
using UnityEngine;

[CreateAssetMenu(fileName = "GamePreset", menuName = "Scriptable Objects/GamePreset")]
public class GamePreset : ScriptableObject
{
    public float LevelMoveSpeed = 10f;
    public float RunnerMoveSpeed = 10f;
    public float RunnerStopTime = 2f;
    [Header("--------Inventory----------")]
    public Vector2Int[] InventoryDimentions;
    public int InventoryCapacity = 5;
    public GameObject InventorySlotPrefab;
    public float HandRotateDuration = 1f;
    public Ease HandRotateEase;
    public GameObject HandPrefab;
    public GameObject CollectionItemPrefab;

    [Header("--------ARTIFACTS----------")]
    public List<ArtifactPreset> ArtifactList;
    public float ArtifactPickupSpeed = 0.5f;
}