using System.Collections;
using Unity.VisualScripting;
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
    private int activeEnemies = 0;

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
    }

    private IEnumerator StartWave()
    {
        isSpawning = true;
        waveId++;

        Debug.Log("Lancement de la vague " + waveId);

        int groundlingsCount = initialGroundlings + waveId * 2;
        int flyingsCount = initialFlyings + waveId;

        yield return StartCoroutine(SpawnEnemies(groundlingPrefab, flyingPrefab, groundlingsCount, flyingsCount, spawnRateG, spawnRateF));

        isSpawning = false;
    }

    private IEnumerator SpawnEnemies(GameObject enemyPrefabG, GameObject enemyPrefabF, int countG, int countF, float spawnRateF, float spawnRateG)
    {
        for (int i = 0; i < countG; i++)
        {
            GameObject enemy = Instantiate(enemyPrefabG, start.position, Quaternion.identity);
            enemy.GetComponent<Enemy>().goal = end;

            // On incrémente le nombre d'ennemis actifs
            activeEnemies++;
            Debug.Log(activeEnemies);

            // On s'abonne à l'événement de destruction de l'ennemi
            enemy.GetComponent<Enemy>().m_destroy += EnemyDied;

            yield return new WaitForSeconds(spawnRateG);
        }

        for (int i = 0; i < countF; i++)
        {
            GameObject enemy = Instantiate(enemyPrefabF, start.position, Quaternion.identity);
            enemy.GetComponent<Enemy>().goal = end;

            // On incrémente le nombre d'ennemis actifs
            activeEnemies++;
            Debug.Log(activeEnemies);

            // On s'abonne à l'événement de destruction de l'ennemi
            enemy.GetComponent<Enemy>().m_destroy += EnemyDied;

            yield return new WaitForSeconds(spawnRateF);
        }
    }

    private void EnemyDied()
    {
        activeEnemies--;
        Debug.Log(activeEnemies);

        // Vérifie si tous les ennemis sont morts et lance une nouvelle vague
        if (activeEnemies == 0 && !isSpawning)
        {
            StartCoroutine(StartWave());
        }
    }
}