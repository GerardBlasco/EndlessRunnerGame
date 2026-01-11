using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager instance;

    [SerializeField] bool playerDead = false;
    [SerializeField] private int points = 0;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject pointsPanel;
    [SerializeField] TextMeshProUGUI pointsText;

    private int difficulty = 0;
    private int score = 0;
    private int scoreValue = 15;

    private float timer, scoreTimer;
    private float timerDelay = 1f;
    private float scoreTimerDelay = 0.5f;

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
        scoreTimer += Time.deltaTime;

        if (timer >= timerDelay)
        {
            timer = 0f;
            EarnPoints();
        }

        if (scoreTimer >= scoreTimerDelay)
        {
            scoreTimer = 0f;
            AddScore();
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

    public IEnumerator EndGame()
    {
        playerDead = true;
        PauseGame();
        pointsPanel.SetActive(false);

        yield return new WaitForSeconds(2f);

        gameOverPanel.SetActive(true);
        scoreText.text = "Total Score: " + score;
    }

    public void AddScore()
    {
        score += scoreValue;
        pointsText.text = "Points: " + score;
    }
}
