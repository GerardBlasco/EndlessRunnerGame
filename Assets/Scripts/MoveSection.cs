using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveSection : MonoBehaviour
{
    [SerializeField] private float moveMagnitude = 4f;
    [SerializeField] private int maxSectionsInGame = 3;
    public static List<GameObject> existingSections = new List<GameObject>();

    private void Start()
    {
        existingSections.Add(gameObject);
    }

    void Update()
    {
        DestroySection();
        _MoveSection();
    }

    public void DestroySection()
    {
        if (existingSections.Count > maxSectionsInGame)
        {
            Destroy(existingSections.First());
            existingSections.RemoveAt(0);
        }
    }

    public void _MoveSection()
    {
        transform.position += new Vector3(0, 0, -moveMagnitude) * Time.deltaTime;
    }
}
