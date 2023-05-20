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

    private void Awake()
    {
        difficulty = PlayerPrefs.GetInt("difficulty");
        pauseMenu.SetActive(false);
        UpdateScoreGUI();
        // UpdateLivesGUI();
    }

    private void Update()
    {
        print(difficulty);
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
        var scoreGUIParent = scoreGUI.transform.parent;
        LeanTween.scale(scoreGUIParent.gameObject, new Vector3(1.1f, 1.1f, 1.1f), 0.3f).setEasePunch();
        LeanTween.scale(scoreGUI, new Vector3(1f, 1f, 1f), 0.3f).setDelay(0.3f);
        LeanTween.scale(scoreGUIParent.gameObject, new Vector3(1f, 1f, 1f), 0.3f).setDelay(0.3f);
        
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