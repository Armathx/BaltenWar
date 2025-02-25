using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    [SerializeField] private List<WavePreset> waveConfigs; // Liste des configurations de vague

    private int waveId = 0;
    private bool isSpawning = false;
    private int activeEnemies = 0;

    public enum ENNEMY_TYPE
    {
        GROUNDLING,
        FLYING,
        COUNT
    }

    [SerializeField] private GameObject[] enemyPrefab = new GameObject[(int)ENNEMY_TYPE.COUNT];

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
    }

    private IEnumerator StartWave()
    {
        if (waveId >= waveConfigs.Count)
            yield break; // Arrête si aucune autre vague n'est configurée.

        isSpawning = true;
        WavePreset currentWaveConfig = waveConfigs.First();
        waveConfigs.RemoveAt(0);

        Debug.Log("Lancement de la vague " + waveId);

        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(SpawnEnemies(currentWaveConfig));

        isSpawning = false;
    }

    private IEnumerator SpawnEnemies(WavePreset waveConfig)
    {
        for (int i = 0; i < waveConfig.count.Count(); i++)
        {
            for (int j = 0; j < waveConfig.count[i]; j++)
            {
                GameObject enemy = Instantiate(enemyPrefab[(int)waveConfig.enemyTypes[i]], start.position, Quaternion.identity);
                enemy.GetComponent<Enemy>().goal = end;

                // Incrémenter le nombre d'ennemis actifs
                activeEnemies++;
                Debug.Log(activeEnemies);

                // S'abonner à l'événement de destruction de l'ennemi
                enemy.GetComponent<Enemy>().m_destroy += EnemyDied;

                yield return new WaitForSeconds(0.1f);
            }
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