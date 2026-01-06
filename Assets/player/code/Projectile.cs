using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed = 6;
    public GameObject explosionPrefab;
    private PointManager pointManager;

    void Start()
    {
        // Upewniamy się, że PointManager istnieje, żeby uniknąć błędów
        GameObject pm = GameObject.Find("PointManager");
        if (pm != null)
        {
            pointManager = pm.GetComponent<PointManager>();
        }
    }

    void Update()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            
            EnemyLoot loot = collision.gameObject.GetComponent<EnemyLoot>();
            if (loot != null)
            {
                loot.CalculateLoot(); 
            }
            // ---------------------------------------------------------

            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            
            
            Destroy(collision.gameObject);
            
            
            if (pointManager != null)
            {
                pointManager.UpdateScore(100);
            }

            
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Sciana")
        {
            Destroy(gameObject);
        }
    }
}