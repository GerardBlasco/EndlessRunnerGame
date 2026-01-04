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

    private float moveSpeed = 0;
    private float maxSpeed = 40f;

    private void Start()
    {
        progressionManager = ProgressionManager.instance;
        existingSections.Add(gameObject);
    }

    void Update()
    {
        DestroySection();
        MoveSection();
        SpeedUp();
    }

    public void DestroySection()
    {
        if (existingSections.Count > maxSectionsInGame)
        {
            Destroy(existingSections.First());
            existingSections.RemoveAt(0);
        }
    }

    public void SpeedUp()
    {
        moveSpeed = moveMagnitude + progressionManager.GetPoints();

        moveSpeed = Mathf.Clamp(moveSpeed, moveMagnitude, maxSpeed);
    }

    public void MoveSection()
    {
        transform.position += new Vector3(0, 0, -moveSpeed) * Time.deltaTime;
    }
}
