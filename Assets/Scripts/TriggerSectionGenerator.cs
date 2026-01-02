using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSectionGenerator : MonoBehaviour
{
    [SerializeField] private GameObject sectionPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WallTrigger"))
        {
            Instantiate(sectionPrefab);
        }
    }
}
