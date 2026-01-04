using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    [SerializeField] bool playerDead = false;
    [SerializeField] private int points = 0;

    void Update()
    {
        if (playerDead) 
        {
            return;
        }

        EarnPoints();
    }

    public void EarnPoints()
    {
        points++;
    }
}
