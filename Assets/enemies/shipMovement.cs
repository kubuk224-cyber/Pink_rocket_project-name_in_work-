using UnityEngine;

public class shipMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag ==  "Sciana")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z );
            moveSpeed *= -1; 
        }

        
    }
}
