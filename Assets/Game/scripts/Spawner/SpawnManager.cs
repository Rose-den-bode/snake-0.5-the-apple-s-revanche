using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;   // Het vijand prefab dat je wilt spawnen
    public float maxActiveEnemies;  // Maximaal aantal vijanden dat tegelijk actief mag zijn
    public float totalEnemiesToSpawn;  // Totaal aantal vijanden dat moet worden gespawnt
    public float spawnInterval;  // Tijd tussen het spawnen van vijanden

    private int currentSpawnedEnemies;  // Huidige aantal gespawnde vijanden
    public int activeEnemies;  // Huidige aantal actieve vijanden
    public GameObject upgrades;
    private GameObject playerObject;        // Verwijzing naar de speler
    private GameObject weaponObject;        // Verwijzing naar de weapon




    void Start()
    {
        maxActiveEnemies = 3f;
        totalEnemiesToSpawn = 5f;
        spawnInterval = 2f;
        currentSpawnedEnemies = 0;
        activeEnemies = 0;

        
        playerObject = GameObject.FindWithTag("Player");
        weaponObject = GameObject.FindWithTag("Weapon");
        NewWave();
    }

    void Update()
    {
        if (currentSpawnedEnemies >= totalEnemiesToSpawn && activeEnemies == 0 && !upgrades.activeSelf)
        {
            Player player = playerObject.GetComponent<Player>();
            Weapon weapon = weaponObject.GetComponent<Weapon>();

            if (player != null && player.SpriteRenderer != null)
            {
                player.SpriteRenderer.enabled = !player.SpriteRenderer.enabled;
                weapon.spriteRenderer.enabled = !weapon.spriteRenderer.enabled;
            }

            if (upgrades != null)
            {
                upgrades.SetActive(!upgrades.activeSelf);
            }
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (currentSpawnedEnemies < totalEnemiesToSpawn)
        {
            if (activeEnemies < maxActiveEnemies)
            {
                SpawnEnemy();
                currentSpawnedEnemies++;
            }
            Debug.Log(activeEnemies);
            yield return new WaitForSeconds(spawnInterval);
        }
        
    }

        void SpawnEnemy()
    {
    // Genereer een willekeurige hoek en positie binnen de cirkel
        float angle = Random.Range(0f, Mathf.PI * 2);
        Vector2 spawnPos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * Random.Range(15f, 20f);
        Vector3 spawnPosition = new Vector3(spawnPos.x, spawnPos.y, 0f) + transform.position;

    // Instantiate het vijand prefab
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

    // Verkrijg het Enemy script van het nieuwe vijand object
        Enemy enemyScript = newEnemy.GetComponent<Enemy>();

    // Geef de SpawnManager referentie door aan het Enemy script
        enemyScript.spawnManager = this;

    // Verhoog het aantal actieve vijanden
        activeEnemies++;
    }

    public void NewWave()
    {
        currentSpawnedEnemies = 0;
        StartCoroutine(SpawnEnemies());
    }


    public void NotifyEnemyDestroyed()
    {
        activeEnemies -= 1;
    }

    
}
