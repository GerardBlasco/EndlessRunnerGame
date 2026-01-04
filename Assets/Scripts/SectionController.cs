using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SectionController : MonoBehaviour
{
    private ProgressionManager progressionManager;
    [SerializeField] private float moveMagnitude = 10f;
    [SerializeField] private int maxSectionsInGame = 3;
    public static List<GameObject> existingSections = new List<GameObject>();
    
    private void Start()
    {
        progressionManager = ProgressionManager.instance;
        existingSections.Add(gameObject);
    }

    void Update()
    {
        DestroySection();
        MoveSection();
    }

    public void DestroySection()
    {
        if (existingSections.Count > maxSectionsInGame)
        {
            Destroy(existingSections.First());
            existingSections.RemoveAt(0);
        }
    }

    public void MoveSection()
    {
        transform.position += new Vector3(0, 0, -moveMagnitude) * Time.deltaTime;
    }
}
