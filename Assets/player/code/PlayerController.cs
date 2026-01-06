using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float hInput;
    
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        
        rb.gravityScale = 0; 
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; 
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; 
    }

    void Update()
    {
        hInput = Input.GetAxis("Horizontal");
    }

    
    void FixedUpdate()
    {
        
        rb.linearVelocity = new Vector2(hInput * moveSpeed, 0); 
    }
}