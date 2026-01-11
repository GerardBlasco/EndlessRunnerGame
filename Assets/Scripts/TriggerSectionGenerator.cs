using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSectionGenerator : MonoBehaviour
{
    private ProgressionManager progressionManager;
    [SerializeField] List<GameObject> sectionPrefabs = new List<GameObject>();
    [SerializeField] private int difficulty = 0;

    private float spawnDistance = 47.65f;

    private void Start()
    {
        progressionManager = ProgressionManager.instance;
    }

    public void AddDifficulty()
    {
        difficulty++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WallTrigger"))
        {
            Instantiate(sectionPrefabs[progressionManager.GetDifficulty()], GameObject.FindGameObjectWithTag("ConnectPoint").transform.position, Quaternion.identity);
            Destroy(GameObject.FindGameObjectWithTag("ConnectPoint"));
            //Instantiate(sectionPrefabs[progressionManager.GetDifficulty()], new Vector3(0, 0, spawnDistance), Quaternion.identity);
        }
    }
}
