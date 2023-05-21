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
	public GameObject gameOverMenu;

    private void Awake()
    {
        difficulty = PlayerPrefs.GetInt("difficulty");
        pauseMenu.SetActive(false);
		gameOverMenu.SetActive(false);
        UpdateScoreGUI();
        UpdateLivesGUI();
    }

    private void Update()
    {
        UpdateScoreGUI();
        UpdateLivesGUI();

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

    public void AddScore(int points, bool isBomb)
    {
        // Score GUI animation
        LeanTween.scale(scoreGUI, new Vector3(1.1f, 1.1f, 1.1f), 0.3f).setEasePunch();
        LeanTween.scale(scoreGUI.transform.parent.gameObject, new Vector3(1.1f, 1.1f, 1.1f), 0.3f).setEasePunch();
        
        score += points;
        
        if (isBomb) return;
        slashSound.GetComponent<AudioSource>().time = 1.2f;
        slashSound.GetComponent<AudioSource>().Play();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void UpdateScoreGUI()
    {
        scoreGUI.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
    
    private void UpdateLivesGUI()
    {
        switch (lives)
        {
            case 3:
                livesGUI.transform.GetChild(0).gameObject.SetActive(true);
                livesGUI.transform.GetChild(1).gameObject.SetActive(true);
                livesGUI.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 2:
                livesGUI.transform.GetChild(0).gameObject.SetActive(true);
                livesGUI.transform.GetChild(1).gameObject.SetActive(true);
                livesGUI.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 1:
                livesGUI.transform.GetChild(0).gameObject.SetActive(true);
                livesGUI.transform.GetChild(1).gameObject.SetActive(false);
                livesGUI.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 0:
                livesGUI.transform.GetChild(0).gameObject.SetActive(false);
                livesGUI.transform.GetChild(1).gameObject.SetActive(false);
                livesGUI.transform.GetChild(2).gameObject.SetActive(false);
                break;
        }
    }

    public void PlayExplosionSound()
    {
        bombSound.GetComponent<AudioSource>().time = 0.2f;
        bombSound.GetComponent<AudioSource>().Play();
		gameOverMenu.SetActive(true);
    }

    public void RemoveLife()
    {
        lives--;
        if (lives <= 0)
        {
            //todo: game over
            // GameOver();
        }
    }
}