using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    [Header("Co może wypaść?")]
    public GameObject[] powerUpPrefabs; 

    [Header("Szansa na drop (0-100%)")]
    public float dropChance = 30f; 

    
    public void CalculateLoot()
    {
        
        float randomValue = Random.Range(0f, 100f);

        
        if (randomValue <= dropChance)
        {
            SpawnPowerUp();
        }
    }

    void SpawnPowerUp()
    {
        if (powerUpPrefabs.Length > 0)
        {
            // Losujemy który przedmiot wypadnie
            int randomIndex = Random.Range(0, powerUpPrefabs.Length);
            
            // Tworzymy go w miejscu wroga
            Instantiate(powerUpPrefabs[randomIndex], transform.position, Quaternion.identity);
        }
    }
}