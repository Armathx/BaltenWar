using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    [SerializeField] private List<WavePreset> waveConfigs; //Wave configurations list

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
        if (waveConfigs.Count == 0)
        {
            PlayerInfo.instance.Win();
            yield break; // Stop if no more wave
        }

        isSpawning = true;
        WavePreset currentWaveConfig = waveConfigs.First();
        waveConfigs.RemoveAt(0);

        Debug.Log("Lancement de la vague " + currentWaveConfig.name);

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

                //Increment enemies count
                activeEnemies++;
                Debug.Log(activeEnemies);

                //Delegate
                enemy.GetComponent<Enemy>().m_destroy += EnemyDied;

                yield return new WaitForSeconds(0.1f); //Spawn Delay
            }
        }
    }

    private void EnemyDied()
    {
        activeEnemies--;
        //Debug.Log(activeEnemies);

        //Check if all enemies are dead
        if (activeEnemies == 0 && !isSpawning)
        {
            StartCoroutine(StartWave());
        }
    }
}