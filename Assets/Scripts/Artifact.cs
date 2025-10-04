using System;
using UnityEngine;

public class Artifact : MonoBehaviour, IArtifact
{
    [SerializeField] private LayerMask _interactionMask;
    public event Action<Artifact> OnArtifactPickedUp;

    public Transform GetTransform()
    {
        return transform;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _interactionMask) != 0)
        {
            OnArtifactPickedUp?.Invoke(this);
        }
    }
}