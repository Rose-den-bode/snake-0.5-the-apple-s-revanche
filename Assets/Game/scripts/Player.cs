using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public TextMeshProUGUI Hp;
    public float invincibilityDuration = 1f;  // Duur van onkwetsbaarheid na een hit
    private bool isInvincible = false;
    private float invincibilityEndTime = 0f;
    public int currentHealth;
    private Collider2D playerCollider;
    public SpriteRenderer SpriteRenderer; // Zorg dat dit correct is gedeclareerd
    public GameObject full;
    public GameObject damage;
    public GameObject low;
    
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = 3;
        playerCollider = GetComponent<Collider2D>();
        UpdateUI();
    }

    void Update()
    {
        // Controleer of de onkwetsbaarheidstijd is verstreken
        if (isInvincible && Time.time > invincibilityEndTime)
        {
            isInvincible = false;
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                isInvincible = true;
                invincibilityEndTime = Time.time + invincibilityDuration;
            }
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        if(currentHealth == 3)
        {
            full.SetActive(true);
            damage.SetActive(false);
            low.SetActive(false);
        }else if(currentHealth == 2)
        {
            full.SetActive(false);
            damage.SetActive(true);
            low.SetActive(false);
        }else if(currentHealth == 1)
        {
            full.SetActive(false);
            damage.SetActive(false);
            low.SetActive(true);
        }else{
            SceneManager.LoadScene("SampleScene");
        }
    }

    void Die()
    {
        Debug.Log("Player has died.");
        gameObject.SetActive(false);
        // Optioneel: Laad een andere scène of toon een game-over scherm
    }
}
