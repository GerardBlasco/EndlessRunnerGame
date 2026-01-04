using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleGeneration : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] int minObstacles = 2;
    [SerializeField] int maxObstacles = 6;

    private int totalObstacles = 0;
    private List<GameObject> obstacleSpawns = new List<GameObject>();

    private void Start()
    {
        totalObstacles = Random.Range(minObstacles, maxObstacles);

        obstacleSpawns = GameObject.FindGameObjectsWithTag("ObstacleSpawn").ToList();
       
        SpawnObstacles();
        DestroyCurrentSpawns();
    }

    public void SpawnObstacles()
    {      
        for (int i = 0; i < totalObstacles; i++)
        {
            int randomSpawnIndex = Random.Range(0, obstacleSpawns.Count - 1);

            Instantiate(obstaclePrefab, obstacleSpawns[randomSpawnIndex].transform.position, Quaternion.identity, transform);
        }
    }

    public void DestroyCurrentSpawns()
    {
        for (int i = 0; i < obstacleSpawns.Count; i++)
        {
            Destroy(obstacleSpawns[i]);
        }
    }
}
