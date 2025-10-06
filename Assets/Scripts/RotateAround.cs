using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public Transform target;
    public float speed = 50f;
    public Vector3 axis = Vector3.up;
    
    void Update()
    {
        if (target != null)
        {
            transform.RotateAround(target.position, axis, speed * Time.deltaTime);
        }
    }
}