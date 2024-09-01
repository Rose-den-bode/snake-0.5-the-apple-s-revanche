using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    public float shootSpeed;   // Tijd tussen schoten (hoe lager, hoe sneller het wapen schiet)
    public int damage;           // Schade per schot
    public int maxAmmo;       // Maximaal aantal kogels voordat je moet herladen
    public float reloadSpeed;    // Tijd om te herladen

    [Header("References")]
    public GameObject bulletPrefab;   // Prefab van de kogel
    public Transform firePoint;       // De positie waar de kogel wordt geschoten
    public SpawnManager spawnManager; // Referentie naar de SpawnManager

    private int currentAmmo;          // Huidige hoeveelheid kogels in het magazijn
    private bool isReloading = false; // Flag om te controleren of het wapen aan het herladen is
    private float lastShotTime;       // Tijd sinds het laatste schot
    private float bulletSpeed = 10f;

    public  SpriteRenderer spriteRenderer;
    public AudioClip gunshotSound;     // Het geluid dat wordt afgespeeld bij het schieten

    void Start()
    {
        shootSpeed = 2f;
        damage = 1;
        maxAmmo = 20000;
        reloadSpeed = 0.1f;

        spriteRenderer = GetComponent<SpriteRenderer>();
        currentAmmo = maxAmmo;        // Begin met een volledig magazijn
        AudioSource audioSource = GetComponent<AudioSource>();
        SoundEffects.Initialize(audioSource);
    }

    void Update()
    {
        RotateTowardsMouse();
        // Als het wapen aan het herladen is, schiet dan niet
        if (isReloading)
            return;

        // Als de linkermuisknop is ingedrukt en er is genoeg tijd verstreken sinds het laatste schot
        if (Input.GetMouseButtonDown(0) && Time.time - lastShotTime >= shootSpeed && spriteRenderer.enabled)
        {
            if (currentAmmo > 0)
            {
                Shoot();
                lastShotTime = Time.time;
            }
            else
            {
                StartCoroutine(Reload());
            }
        }
    }
    void RotateTowardsMouse()
    {
        // Verkrijg de positie van de muis in wereldcoördinaten
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;
        
        // Bereken de hoek en draai het wapen
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Shoot()
    {
        // Spawn de kogel bij de vuurpositie
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Geef de SpawnManager referentie door aan de Bullet
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.spawnManager = spawnManager;
        
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;

        SoundEffects.PlayClip(gunshotSound);


        currentAmmo--; // Verlaag het aantal kogels in het magazijn
    }

    IEnumerator Reload()
    {
        isReloading = true; // Zet de herlaad-flag aan

        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadSpeed); // Wacht de duur van de herlaadsnelheid

        currentAmmo = maxAmmo; // Vul het magazijn bij
        isReloading = false; // Zet de herlaad-flag uit
    }
}
