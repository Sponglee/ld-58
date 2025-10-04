using UnityEngine;
using Zenject;

public class ArtifactFactory : IArtifactFactory
{
    private readonly DiContainer _container;
    
    public ArtifactFactory(DiContainer container)
    {
        _container = container;
    }
  
    public Artifact Create(GameObject prefab, Vector3 position)
    {
        var artifact = _container.InstantiatePrefabForComponent<Artifact>(prefab);
        artifact.GetTransform().position = position;
        return artifact;
    }
}