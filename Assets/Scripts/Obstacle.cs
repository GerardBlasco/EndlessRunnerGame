using UnityEngine;

[System.Serializable]
public class Obstacle
{
    [SerializeField] GameObject prefab;
    [SerializeField] int weight = 1;

    public GameObject GetPrefab()
    {
        return prefab;
    }

    public int GetWeight()
    {
        return weight;
    }
}
