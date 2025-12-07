using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [Tooltip("Miejsca, w których mogą się pojawić wrogowie")]
    public Transform[] spawnPoints;
    public Transform enemyGroupParent;
    [Tooltip("Prefaby wrogów")]
    public GameObject[] enemyPrefabs;
    [Tooltip("Co ile sekund sprawdzać, czy wszystkie pola są puste")]
    public float checkInterval = 1f;

    private float timer = 0f;

    void Start()
    {
        SpawnAllRandom();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= checkInterval) // <- poprawna nazwa zmiennej
        {
            timer = 0f;

            if (AllSpawnPointsEmpty())
            {
                SpawnAllRandom();
            }
        }
    }

    // spawn na KAŻDYM punkcie i ustawiamy jako child, żeby łatwo sprawdzać childCount
    void SpawnAllRandom()
    {
        foreach (Transform point in spawnPoints)
        {
            int randEnemy = Random.Range(0, enemyPrefabs.Length);
            // ustawiamy parent na point, dzięki temu point.childCount będzie > 0 dopóki obiekt istnieje
            Instantiate(enemyPrefabs[randEnemy], point.position, Quaternion.identity, enemyGroupParent);
        }
    }

    // sprawdza czy wszystkie spawnpointy nie mają dzieci (czyli wrogów)
    bool AllSpawnPointsEmpty()
    {
        foreach (Transform point in spawnPoints)
        {
            if (point.childCount > 0)
                return false;
        }
        return true;
    }
}