using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLives : MonoBehaviour
{
    [Header("Ustawienia Zdrowia")]
    public int lives = 3;
    public int maxLives = 3;

    [Header("UI i Efekty")]
    public Image[] livesUI;
    public GameObject explosionPrefav;

    [Header("Grafiki")]
    public Sprite shieldSprite;   
    public Sprite[] damageSprites; 
    

    private SpriteRenderer spriteRenderer;
    private PlayerPowerUps powerUpsScript;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        powerUpsScript = GetComponent<PlayerPowerUps>();
        
        UpdateLivesUI();
        UpdateShipVisuals();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy Projectile" || collision.gameObject.tag == "Enemy")
        {
            // --- 1. LOOT ---
            if (collision.gameObject.tag == "Enemy")
            {
                EnemyLoot loot = collision.gameObject.GetComponent<EnemyLoot>();
                if (loot != null) loot.CalculateLoot();
            }

            // --- 2. OBSŁUGA TARCZY ---
            if (powerUpsScript != null && powerUpsScript.shieldActive)
            {
                
                Destroy(collision.gameObject); 
                
                
                Debug.Log("Wróg rozbił się o tarczę!");
                
                return; 
            }

            // --- 3. OBSŁUGA OBRYWANIA (BRAK TARCZY) ---
            
            
            Destroy(collision.gameObject); 
            Instantiate(explosionPrefav, transform.position, Quaternion.identity); 
            
            lives -= 1; 
            
            UpdateLivesUI();
            UpdateShipVisuals();

            if (lives <= 0)
            {
                Die();
            }
        }
    }

    public void Heal(int amount)
    {
        lives += amount;
        if (lives > maxLives) lives = maxLives;
        UpdateLivesUI();
        UpdateShipVisuals();
    }

    void UpdateLivesUI()
    {
        for (int i = 0; i < livesUI.Length; i++)
        {
            livesUI[i].enabled = (i < lives);
        }
    }

    
    public void UpdateShipVisuals()
    {
        
        if (powerUpsScript != null && powerUpsScript.shieldActive && shieldSprite != null)
        {
            spriteRenderer.sprite = shieldSprite;
            return; 
        }

        
        if (lives > 0 && damageSprites.Length >= lives)
        {
            spriteRenderer.sprite = damageSprites[lives - 1];
        }
    }

    void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(3);
    }
}