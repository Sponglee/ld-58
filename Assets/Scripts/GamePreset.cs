using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GamePreset", menuName = "Scriptable Objects/GamePreset")]
public class GamePreset : ScriptableObject
{
    public float LevelMoveSpeed = 10f;
    public float RunnerMoveSpeed = 10f;

}
