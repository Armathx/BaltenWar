using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    [SerializeField] private GameObject flyingPrefab;
    [SerializeField] private GameObject groundlingPrefab;

    [SerializeField] private int initialGroundlings = 10;
    [SerializeField] private int initialFlyings = 5;
    [SerializeField] private float spawnRateG = 0.5f;
    [SerializeField] private float spawnRateF = 1f;
    [SerializeField] private float waveInterval = 5f;

    private int waveId = 0;
    private bool isSpawning = false;

    private void Update()
    {
        if (!isSpawning)
        {
            StartCoroutine(StartWave());
        }
    }

    private IEnumerator StartWave()
    {
        isSpawning = true;
        waveId++;

        Debug.Log("Lancement de la vague " + waveId);

        int groundlingsCount = initialGroundlings + waveId * 2; //Incrémente de façon chelou les count de mobs
        int flyingsCount = initialFlyings + waveId;

        yield return StartCoroutine(SpawnEnemies(groundlingPrefab, groundlingsCount, spawnRateG));
        yield return StartCoroutine(SpawnEnemies(flyingPrefab, flyingsCount, spawnRateF));

        yield return new WaitForSeconds(waveInterval);

        isSpawning = false;
    }

    private IEnumerator SpawnEnemies(GameObject enemyPrefab, int count, float spawnRate)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, start.position, Quaternion.identity);
            enemy.GetComponent<Enemy>().goal = end;
            yield return new WaitForSeconds(spawnRate);
        }
    }
}