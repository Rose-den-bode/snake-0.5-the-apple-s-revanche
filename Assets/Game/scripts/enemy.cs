using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;      // Snelheid waarmee de vijand beweegt
    public int damage = 10;       // Schade die de vijand aan de speler toebrengt
    public SpawnManager spawnManager; // Referentie naar de SpawnManager

    private Transform player;     // Referentie naar de speler

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
            RotateTowardsPlayer(direction);
        }
    }

    void RotateTowardsPlayer(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(damage);
                Die();
            }
        }
    }

    public void Die()
{
    // Informeer de SpawnManager dat deze vijand is vernietigd
    spawnManager.NotifyEnemyDestroyed();
    
    // Debugging: controleer het aantal actieve vijanden in de SpawnManager
    Debug.Log(spawnManager.activeEnemies);

    Destroy(gameObject);
}

}
