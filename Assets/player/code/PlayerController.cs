using UnityEngine;

// To sprawi, że Unity samo doda Rigidbody2D, jeśli zapomnisz
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float hInput;
    
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Zabezpieczenia fizyki w kodzie (dla pewności)
        rb.gravityScale = 0; // Wyłączamy grawitację (żeby nie spadał)
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Blokujemy obracanie się
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // Lepsze wykrywanie kolizji
    }

    void Update()
    {
        // Pobieramy wejście w Update (tutaj jest najpłynniej)
        hInput = Input.GetAxis("Horizontal");
    }

    // Fizykę zawsze wykonujemy w FixedUpdate
    void FixedUpdate()
    {
        // Ustawiamy prędkość (Velocity).
        // Dzięki temu, gdy uderzymy w ścianę, silnik fizyczny nas zatrzyma.
        rb.linearVelocity = new Vector2(hInput * moveSpeed, 0); 
    }
}