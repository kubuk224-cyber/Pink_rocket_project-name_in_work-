using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLives : MonoBehaviour
{
   public int lives = 3;
    public Image[] livesUI;
    public GameObject explosionPrefav;

    // Usuwamy OnCollisionEnter2D, bo wrogowie są teraz Triggerami!
    
    // Wszystko obsługujemy tutaj
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Sprawdzamy: Czy to wrogi pocisk LUB czy to sam wróg (statek)
        if (collision.gameObject.tag == "Enemy Projectile" || collision.gameObject.tag == "Enemy")
        {
            // 1. Zniszcz to, co w nas uderzyło (pocisk lub wroga)
            Destroy(collision.gameObject);
            
            // 2. Stwórz efekt wybuchu
            Instantiate(explosionPrefav, transform.position, Quaternion.identity);
            
            // 3. Odejmij życie
            lives -= 1;
            
            // 4. Aktualizacja UI (serduszek)
            UpdateLivesUI();

            // 5. Sprawdzenie przegranej
            if (lives <= 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene(3);
            }
        }
    }

    // Wydzieliłem to do osobnej funkcji dla czystości kodu
    void UpdateLivesUI()
    {
        for (int i = 0; i < livesUI.Length; i++)
        {
            if (i < lives)
            {
                livesUI[i].enabled = true;
            }
            else
            {
                livesUI[i].enabled = false;
            }
        }
    }
    
}
