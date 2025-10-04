    using UnityEngine;

    public class Runner : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        public void MoveRunner(Vector3 move)
        {
            _rigidbody.linearVelocity = move;
        }
    }