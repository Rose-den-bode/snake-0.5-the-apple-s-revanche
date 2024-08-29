using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 targetPosition;  // The target position from the cursor
    private float speed = 10f;       // Speed of the projectile
    private float damage;

    // Function to set the target position and damage when the projectile is instantiated
    public void Seek(Vector3 _targetPosition, float _damage)
    {
        targetPosition = _targetPosition;
        damage = _damage;
    }

    void Update()
    {
        // Move the projectile towards the target position
        Vector3 direction = targetPosition - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // Check if the projectile reaches the target position
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Move the projectile in the direction of the target
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(targetPosition); // Optional: orient projectile towards target
    }

    // Function to handle impact
    void HitTarget()
    {
        // Implement damage or impact effect if needed
        Destroy(gameObject); // Destroy the projectile on impact
        // Add additional impact effects or damage handling here
    }
}
