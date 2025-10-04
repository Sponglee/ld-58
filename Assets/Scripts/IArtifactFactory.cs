using UnityEngine;

public interface IArtifactFactory
{
    Artifact Create(GameObject prefab, Vector3 position);
}