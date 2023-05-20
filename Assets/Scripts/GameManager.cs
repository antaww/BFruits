using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public int score;
    public int lives = 3;
    public static bool IsPaused;
    
    public GameObject scoreGUI;
    public GameObject livesGUI;
    public GameObject pauseMenu;
    public GameObject slashSound;

    private void Awake()
    {
        pauseMenu.SetActive(false);
        UpdateScoreGUI();
        // UpdateLivesGUI();
    }
    
    private void Update()
    {
        UpdateScoreGUI();
        // UpdateLivesGUI();

        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (IsPaused)
        {
            Pause.ResumeGame();
        }
        else
        {
            Pause.PauseGame();
        }
    }

    public void AddScore(int points)
    {
        score += points;
        slashSound.GetComponent<AudioSource>().time = 1.2f;
        slashSound.GetComponent<AudioSource>().Play();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void UpdateScoreGUI()
    {
        scoreGUI.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
}
