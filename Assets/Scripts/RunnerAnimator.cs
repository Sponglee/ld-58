using UnityEngine;

public class RunnerAnimator : MonoBehaviour
{
    [SerializeField] private Transform runnerModel;

    [SerializeField] private float animationPeriod;

    private float timer = 0f;
    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > animationPeriod)
        {
            FlipTransform();
            timer = 0f;
        }
    }

    private void FlipTransform()
    {
        runnerModel.transform.localScale = new Vector3(
            -transform.localScale.x,
            transform.localScale.y,
            transform.localScale.z);
    }
}
