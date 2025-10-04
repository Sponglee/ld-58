    using UnityEngine;

    public class RunnerView : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _artifactSpot;
        
        public Transform ArtifactSpot => _artifactSpot;
        
        public void MoveRunner(Vector3 move)
        {
            _rigidbody.linearVelocity = move;
        }
    }