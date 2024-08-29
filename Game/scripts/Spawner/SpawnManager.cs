using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;   // Het vijand prefab dat je wilt spawnen
    public int numberOfEnemies = 5;  // Aantal vijanden om te spawnen
    public float minSpawnRadius = 15f;  // Minimale straal van het spawngebied
    public float maxSpawnRadius = 20f;  // Maximale straal van het spawngebied

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        Vector3 spawnCenter = transform.position; // Positie van het SpawnManager GameObject

        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Genereer een willekeurige hoek in radialen
            float angle = Random.Range(0f, Mathf.PI * 2);

            // Kies een willekeurige afstand binnen het opgegeven bereik
            float radius = Random.Range(minSpawnRadius, maxSpawnRadius);
            
            // Bepaal de positie binnen de cirkel in 2D (x, y)
            Vector2 spawnPos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

            // Zet de 2D positie om naar een 3D Vector, maar gebruik alleen x en y
            Vector3 spawnPosition = new Vector3(spawnPos.x, spawnPos.y, 0) + spawnCenter;

            // Debugging informatie
            Debug.Log($"Spawning enemy at: {spawnPosition}");

            // Spawn de vijand
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
