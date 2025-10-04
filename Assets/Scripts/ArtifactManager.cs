using System;
using System.Collections.Generic;

public class ArtifactManager: IDisposable
{
    public event Action<Artifact> OnArtifactTriggered;
    
    private List<Artifact> _activeArtifacts = new List<Artifact>();

    public void AddArtifact(Artifact artifact)
    {
        _activeArtifacts.Add(artifact);
        artifact.OnArtifactPickedUp += ArtifactPickedUp;
    }
    
    public void Dispose()
    {
        foreach (var artifact in _activeArtifacts)
        {
            artifact.OnArtifactPickedUp -= ArtifactPickedUp;
        }    
    }

    private void ArtifactPickedUp(Artifact artifact)
    {
        OnArtifactTriggered?.Invoke(artifact);
        
    }

   
}