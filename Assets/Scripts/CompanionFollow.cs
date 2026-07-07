using UnityEngine;

public class CompanionFollow : MonoBehaviour
{
    public Transform target;
    public float followDistance = 3f;
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;

    void Update()
    {
        if (target == null) return;

        Vector3 directionToTarget = target.position - transform.position;
        float distance = directionToTarget.magnitude;

        if (distance > followDistance)
        {
            Vector3 moveDirection = directionToTarget.normalized;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}