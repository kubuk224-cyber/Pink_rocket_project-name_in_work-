using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject enemyProjectile;
    public float spawnTimer;
    public float spawnMax = 10;
    public float spawnMin = 5;

    void Start()
    {
        spawnTimer = Random.Range(spawnMin, spawnMax); 
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        
        if(spawnTimer <= 0)
        {
            // Zmiana: Wywołujemy funkcję strzelania tylko jeśli są jacyś wrogowie
            if (transform.childCount > 0)
            {
                SpawnProjectile();
            }
            
            spawnTimer = Random.Range(spawnMin, spawnMax);
        }
    }

    void SpawnProjectile()
    {
        // 1. Losujemy numer indeksu dziecka (od 0 do liczby dzieci)
        int randomChildIndex = Random.Range(0, transform.childCount);

        // 2. Pobieramy konkretnego wroga (Transform) o tym numerze
        Transform randomEnemy = transform.GetChild(randomChildIndex);

        // 3. Tworzymy pocisk w pozycji TEGO KONKRETNEGO wroga
        Instantiate(enemyProjectile, randomEnemy.position, Quaternion.identity);
    }
}