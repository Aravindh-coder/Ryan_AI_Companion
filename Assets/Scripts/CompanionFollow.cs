using UnityEngine;

public class CompanionFollow : MonoBehaviour
{
    public Transform target;
    public float followDistance = 3f;
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (target == null) return;

        Vector3 directionToTarget = target.position - transform.position;
        float distance = directionToTarget.magnitude;

        bool isMoving = false;

        if (distance > followDistance)
        {
            Vector3 moveDirection = directionToTarget.normalized;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            isMoving = true;
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", isMoving ? 1f : 0f);
        }
    }
}