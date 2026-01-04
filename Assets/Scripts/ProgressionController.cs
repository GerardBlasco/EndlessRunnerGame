using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager instance;

    [SerializeField] bool playerDead = false;
    [SerializeField] private int points = 0;

    private int difficulty = 0;

    private float timer;
    private float timerDelay = 1f;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (playerDead) 
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= timerDelay)
        {
            timer = 0f;
            EarnPoints();
        }
        
    }

    public void EarnPoints()
    {
        points++;

        if (points == 10 || points == 20)
        {
            difficulty++;
        }
    }

    public int GetDifficulty()
    {
        return difficulty;
    }
}
