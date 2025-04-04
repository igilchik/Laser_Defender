using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave config", fileName = "New Wave Config")]
public class WaveConfig : ScriptableObject
{
    [Header("Path settings")]
    [SerializeField] private Transform pathPrefab;

    [Header("Enemy movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Spawning settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int numberOfEnemies = 5;
    [SerializeField] private float spawnInterval = 2f;

    public float GetMoveSpeed() => moveSpeed;

    public Transform GetStartingPoint() => pathPrefab.GetChild(0);

    public List<Transform> GetPoints()
    {
        List<Transform> wayPoints = new List<Transform>();
        foreach (Transform bod in pathPrefab)
        {
            wayPoints.Add(bod);
        }
        return wayPoints;
    }

    public GameObject GetEnemyPrefab() => enemyPrefab;
    public int GetNumberOfEnemies() => numberOfEnemies;
    public float GetSpawnInterval() => spawnInterval;
}
