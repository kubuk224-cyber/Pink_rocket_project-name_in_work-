using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    [Header("Co może wypaść?")]
    public GameObject[] powerUpPrefabs; // Przeciągnij tu prefabry (Apteczka, Tarcza itp.)

    [Header("Szansa na drop (0-100%)")]
    public float dropChance = 30f; // Np. 30% szans

    // Tę funkcję wywołamy tuż przed zniszczeniem wroga
    public void CalculateLoot()
    {
        // Losujemy liczbę od 0 do 100
        float randomValue = Random.Range(0f, 100f);

        // Jeśli wylosowana liczba jest mniejsza niż szansa (np. wylosowano 15, a szansa to 30)
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