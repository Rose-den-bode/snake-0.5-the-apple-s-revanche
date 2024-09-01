using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    public GameObject upgrades;
    public Weapon weapon;
    public Player player;

    private GameObject playerObject;        // Verwijzing naar de speler
    private GameObject weaponObject;        // Verwijzing naar de weapon
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        weaponObject = GameObject.FindWithTag("Weapon");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpeedUpgrade()
    {
        weapon.shootSpeed *= 0.7f;
        Debug.Log(weapon.shootSpeed);
        resumeGame();
    }
    
    public void DamageUpgrade()
    {
        weapon.damage++;
        Debug.Log(weapon.damage);
        resumeGame();

    }

    public void Heal()
    {
        if(player.currentHealth < 3)
        {
            player.currentHealth++;
            player.UpdateUI();
            resumeGame();
        }
    }

    void resumeGame()
    {
        Player player = playerObject.GetComponent<Player>();
        Weapon weapon = weaponObject.GetComponent<Weapon>();

        upgrades.SetActive(!upgrades.activeSelf);
        player.SpriteRenderer.enabled = !player.SpriteRenderer.enabled;
        weapon.spriteRenderer.enabled = !weapon.spriteRenderer.enabled;

        spawnManager.maxActiveEnemies *= 1.1f;
        spawnManager.totalEnemiesToSpawn *= 1.2f;
        spawnManager.spawnInterval *= 0.95f;
        spawnManager.NewWave();
    }
    
}
