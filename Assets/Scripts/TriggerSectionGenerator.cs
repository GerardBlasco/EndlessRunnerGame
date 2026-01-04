using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSectionGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> sectionPrefabs = new List<GameObject>();
    [SerializeField] private int difficulty = 0;

    public void AddDifficulty()
    {
        difficulty++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WallTrigger"))
        {
            Instantiate(sectionPrefabs[difficulty], new Vector3(0, 0, 20f), Quaternion.identity);
        }
    }
}
