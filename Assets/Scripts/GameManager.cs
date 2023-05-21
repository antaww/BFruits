using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public int score;
    public int lives = 3;
    public int difficulty;
    public static bool IsPaused;

    public GameObject scoreGUI;
    public GameObject livesGUI;
    
    public GameObject pauseMenu;
    public GameObject slashSound;
    public GameObject bombSound;

    private void Awake()
    {
        difficulty = PlayerPrefs.GetInt("difficulty");
        pauseMenu.SetActive(false);
        UpdateScoreGUI();
        // UpdateLivesGUI();
    }

    private void Update()
    {
        UpdateScoreGUI();
        // UpdateLivesGUI();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Pause.ResumeGame();
            }
            else
            {
                Pause.PauseGame();
            }
        }
    }

    public void AddScore(int points)
    {
        // Score GUI animation
        LeanTween.scale(scoreGUI, new Vector3(1.1f, 1.1f, 1.1f), 0.3f).setEasePunch();
        LeanTween.scale(scoreGUI.transform.parent.gameObject, new Vector3(1.1f, 1.1f, 1.1f), 0.3f).setEasePunch();
        
        score += points;
        
        slashSound.GetComponent<AudioSource>().time = 1.2f;
        slashSound.GetComponent<AudioSource>().Play();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void UpdateScoreGUI()
    {
        scoreGUI.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }

    public void PlayExplosionSound()
    {
        bombSound.GetComponent<AudioSource>().time = 0.2f;
        bombSound.GetComponent<AudioSource>().Play();
    }
}