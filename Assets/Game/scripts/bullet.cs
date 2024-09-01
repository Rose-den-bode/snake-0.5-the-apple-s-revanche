using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f;
    public SpawnManager spawnManager;  // Referentie naar de SpawnManager
    public int damage;

    void Start()
    {
        Destroy(gameObject, lifetime);  // Vernietig de kogel na een bepaalde tijd
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Controleer of de kogel een vijand raakt
        if (other.CompareTag("Enemy"))
        {
            // Informeer de SpawnManager dat een vijand is vernietigd
            spawnManager.NotifyEnemyDestroyed();
            
            // Vernietig de vijand en de kogel
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
