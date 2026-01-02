using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { Shield, Medkit, SpeedBoost, Nuke };
    
    [Header("Ustawienia")]
    public PowerUpType type;
    public float fallSpeed = 2f; // Lekko zwiększyłem prędkość, 0.25f to bardzo wolno

    void Update()
    {
        // Power-up spada w dół
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
        
        // Opcjonalnie: niszcz powerup, gdy spadnie za nisko (poza ekran)
        if (transform.position.y < -6f) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerPowerUps player = collision.GetComponent<PlayerPowerUps>();
            
            if (player != null)
            {
                switch(type)
                {
                    case PowerUpType.Shield:
                        player.ActivateShield();
                        break;

                    case PowerUpType.Medkit:
                        player.Heal(1); // To wywoła funkcję w PlayerLives i zaktualizuje UI!
                        break;

                    case PowerUpType.SpeedBoost:
                        player.ActivateSpeedBoost(5f);
                        break;

                    case PowerUpType.Nuke:
                        player.ActivateNuke();
                        break;
                }
            }
            // Efekt dźwiękowy zbierania można dodać tutaj
            Destroy(gameObject);
        }
    }
}