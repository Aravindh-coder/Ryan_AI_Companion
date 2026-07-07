using UnityEngine;

public class CompanionFollow : MonoBehaviour
{
    public Transform target;          // Assign SeatPoint here in Inspector
    public Transform carRoot;         // Assign the car's root Transform here (the one that moves/drives)
    public float followDistance = 0.3f; // How close counts as "arrived" - keep this small since target IS the seat
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;

    private Animator animator;
    private bool isSeated = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isSeated) return; // once seated, stop all follow/move logic entirely
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
        else
        {
            // Arrived at the seat - snap into place and sit
            SitDown();
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", isMoving ? 1f : 0f);
        }
    }

    void SitDown()
    {
        isSeated = true;

        // Snap exactly to seat position/rotation
        transform.position = target.position;
        transform.rotation = target.rotation;

        // Play sitting animation
        if (animator != null)
        {
            animator.SetFloat("Speed", 0f);
            animator.Play("Sitting Idle");
        }

        // Parent to the car so he moves with it
        if (carRoot != null)
        {
            transform.SetParent(carRoot);
        }
        else
        {
            transform.SetParent(target.parent); // fallback: parent to whatever SeatPoint's parent is (the car)
        }
    }
}