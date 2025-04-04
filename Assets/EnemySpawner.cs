using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] private List<WaveConfig> waveConfigs;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private bool looping = true;

    private int currentWaveIndex = 0;

    void Start()
    {
        StartCoroutine(SpawnAllWaves());
    }

    IEnumerator SpawnAllWaves()
{
    do
    {
        WaveConfig currentWave = waveConfigs[currentWaveIndex];

        // ✅ Сообщаем GameManager, какая волна
        if (GameManager.Instance != null)
            GameManager.Instance.SetWave(currentWaveIndex + 1);

        yield return StartCoroutine(SpawnEnemiesInWave(currentWave));

        currentWaveIndex++;
        if (currentWaveIndex >= waveConfigs.Count)
            currentWaveIndex = 0;

        yield return new WaitForSeconds(timeBetweenWaves);

    } while (looping);
}

    IEnumerator SpawnEnemiesInWave(WaveConfig config)
    {
        for (int i = 0; i < config.GetNumberOfEnemies(); i++)
        {
            GameObject enemy = Instantiate(
                config.GetEnemyPrefab(),
                config.GetStartingPoint().position,
                Quaternion.identity
            );

            PathFinder path = enemy.GetComponent<PathFinder>();
            if (path != null)
                path.myConfig = config;
            else
                Debug.LogError("Враг не содержит компонент PathFinder");

            yield return new WaitForSeconds(config.GetSpawnInterval());
        }
    }
}
