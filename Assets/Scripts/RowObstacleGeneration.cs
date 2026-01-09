using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RowObstacleGeneration : MonoBehaviour
{
    [SerializeField] List<Obstacle> obstaclePrefabs = new List<Obstacle>();

    private List<Transform> obstacleSpawns = new List<Transform>();

    private int freeSlots;
    private int maxObstacles;

    void Start()
    {
        foreach (Transform child in transform)
        {
            obstacleSpawns.Add(child);
        }

        freeSlots = obstacleSpawns.Count;

        SpawnObstacles();
    }

    public void SpawnObstacles()
    {
        int obstaclesToSpawn = Random.Range(1, obstacleSpawns.Count);

        for (int i = 0; i < obstaclesToSpawn; i++)
        {
            int spawnIndex = Random.Range(0, obstacleSpawns.Count);

            GameObject obstaclePrefab = GetRandomObstacleByWeight();

            Instantiate(obstaclePrefab, obstacleSpawns[spawnIndex].position, Quaternion.identity, transform);

            obstacleSpawns.RemoveAt(spawnIndex);
        }
    }

    GameObject GetRandomObstacleByWeight()
    {
        int totalWeight = 0;

        foreach (var obstacle in obstaclePrefabs)
            totalWeight += obstacle.GetWeight();

        int randomValue = Random.Range(0, totalWeight);

        foreach (var obstacle in obstaclePrefabs)
        {
            randomValue -= obstacle.GetWeight();
            if (randomValue < 0)
                return obstacle.GetPrefab();
        }

        return obstaclePrefabs[0].GetPrefab(); // fallback
    }
}
