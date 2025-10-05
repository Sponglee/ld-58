using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradePreset", menuName = "Scriptable Objects/UpgradePreset")]
public class UpgradePreset : ScriptableObject
{
    public List<UpgradePair> Upgrades;
}


[Serializable]
public class UpgradePair
{
    public string Key;
    public UpgradeData UpgradeData;
}
