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
    public Sprite shieldSprite;   // <-- TU PRZYPISZ GRAFIKĘ STATKU Z TARCZĄ
    public Sprite[] damageSprites; 
    // Element 0 = 1 życie, Element 1 = 2 życia, Element 2 = 3 życia

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
            // --- 1. OBSŁUGA DROPU (LOOT) ---
            // Niezależnie czy mamy tarczę czy nie, wróg powinien spróbować wyrzucić przedmiot przed śmiercią
            if (collision.gameObject.tag == "Enemy")
            {
                EnemyLoot loot = collision.gameObject.GetComponent<EnemyLoot>();
                if (loot != null) loot.CalculateLoot();
            }

            // --- 2. OBSŁUGA TARCZY ---
            if (powerUpsScript != null && powerUpsScript.shieldActive)
            {
                // Masz tarczę? Wróg/Pocisk ginie, ale Ty NIE tracisz życia.
                Destroy(collision.gameObject); 
                
                // (Opcjonalnie) Tu możesz dodać efekt dźwiękowy uderzenia w tarczę
                Debug.Log("Wróg rozbił się o tarczę!");
                
                return; // <--- WAŻNE: Wychodzimy z funkcji, żeby nie odjęło życia poniżej
            }

            // --- 3. OBSŁUGA OBRYWANIA (BRAK TARCZY) ---
            // Jeśli dotarliśmy tutaj, znaczy że nie ma tarczy -> obrywamy.
            
            Destroy(collision.gameObject); // Wróg ginie
            Instantiate(explosionPrefav, transform.position, Quaternion.identity); // Wybuch
            
            lives -= 1; // Tracimy życie
            
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

    // Zmieniłem na PUBLIC, żeby PlayerPowerUps mógł to wywołać
    public void UpdateShipVisuals()
    {
        // PRIORYTET 1: Jeśli jest tarcza, pokaż grafikę tarczy
        if (powerUpsScript != null && powerUpsScript.shieldActive && shieldSprite != null)
        {
            spriteRenderer.sprite = shieldSprite;
            return; // Kończymy funkcję, nie sprawdzamy zniszczeń
        }

        // PRIORYTET 2: Jeśli nie ma tarczy, pokaż stan zniszczeń
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