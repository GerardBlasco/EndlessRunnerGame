using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager instance;

    [SerializeField] bool playerDead = false;
    [SerializeField] private int points = 0;
    [SerializeField] GameObject gameOverPanel;

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

    public int GetPoints()
    {
        return points;
    }

    public void PauseGame()
    {
        SectionController[] sectionsInGame = FindObjectsOfType<SectionController>();

        for (int i = 0; i < sectionsInGame.Length; i++)
        {
            sectionsInGame[i].StopSectionMovement();
        }
    }

    public void EndGame()
    {
        gameOverPanel.SetActive(true);
        PauseGame();
    }
}
