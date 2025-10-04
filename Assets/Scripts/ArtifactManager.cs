using System;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactManager: IDisposable
{
    public event Action<Artifact> OnArtifactTriggered;
    
    private Dictionary<Artifact, InventoryItemData> _activeArtifacts = new Dictionary<Artifact, InventoryItemData>();

    public void AddArtifact(InventoryItemData itemData, Artifact artifact)
    {
        _activeArtifacts.Add(artifact, itemData);
        artifact.OnArtifactPickedUp += ArtifactPickedUp;
    }
    
    public void Dispose()
    {
        foreach (var artifactPair in _activeArtifacts)
        {
            artifactPair.Key.OnArtifactPickedUp -= ArtifactPickedUp;
        }    
    }

    private void ArtifactPickedUp(Artifact artifact)
    {
        OnArtifactTriggered?.Invoke(artifact);
    }


    public InventoryItemData GetDataByArtifact(Artifact artifact)
    {
        var artifactData = _activeArtifacts[artifact];
        return artifactData;
    }

    public void DestroyArtifact(Artifact artifact)
    {
        var removed = _activeArtifacts.Remove(artifact);

        if (removed)
        {
            GameObject.Destroy(artifact.gameObject);
        }
    }
}