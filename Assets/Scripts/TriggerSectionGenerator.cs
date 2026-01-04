using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSectionGenerator : MonoBehaviour
{
    [SerializeField] private GameObject sectionPrefab1;
    [SerializeField] private GameObject sectionPrefab2;
    [SerializeField] private GameObject sectionPrefab3;

    [SerializeField] List<GameObject> sectionPrefabs = new List<GameObject>();

    private GameObject activeSection;
    private int difficulty = 0;

    public void AddDifficulty()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WallTrigger"))
        {
            Instantiate(sectionPrefabs[difficulty], new Vector3(0, 0, 20f), Quaternion.identity);
        }
    }
}
