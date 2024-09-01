using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShooting : MonoBehaviour
{
    [Header("Tower Attributes")]
    public float fireRate = 1f;             // Shots per second
    public float damage = 20f;              // Damage per shot
    public GameObject projectilePrefab;     // Projectile prefab
    public Transform firePoint;             // Point where the projectile spawns

    private float fireCooldown = 0f;        // Time left until the next shot

    void Update()
    {
        // Check if the player clicks and the cooldown is over
        if (Input.GetMouseButtonDown(0) && fireCooldown <= 0f) // Left mouse button click
        {
            Shoot();
            fireCooldown = 1f / fireRate; // Reset the cooldown
        }

        // Reduce the cooldown over time
        fireCooldown -= Time.deltaTime;
    }

    // Function to handle shooting
    void Shoot()
    {
        // Get the mouse position in the world
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 targetPosition;

        // Check if the ray hits something in the world
        if (Physics.Raycast(ray, out hit))
        {
            targetPosition = hit.point; // Use the point where the ray hits
        }
        else
        {
            // If no hit, shoot in the direction of the ray
            targetPosition = ray.GetPoint(100); // Arbitrary distance if nothing is hit
        }

        // Instantiate the projectile and set its direction
        GameObject projectileGO = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();

        // Check if the projectile script exists and set its target direction
        if (projectile != null)
        {
            projectile.Seek(targetPosition, damage);
        }
    }
}
