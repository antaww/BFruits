using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// Librarys hinzufügen, um mit UI-Elementen und SceneManager arbeiten zu können
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Variablen deklarieren
    int score;
    int lives;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public bool gameIsOver;

    // Startwerte für score und lives festlegen
    void Start()
    {
        score = 0;
        lives = 3;
    }

    // Erhöht den Score um die Anzahl der Punkte, die der Fruit zugewiesen wurden
    public void UpdateTheScore(int scorePointsToAdd)
    {
        score += scorePointsToAdd;
        scoreText.text = score.ToString();
    }

    // Zieht ein Leben ab, wenn eine Fruit verpasst wurde und beendet das Spiel wenn keine Leben mehr übrig sind
    public void UpdateLives()
    {
        if(gameIsOver == false)
        {
            lives--;
            livesText.text = "Lives: " + lives;

            if (lives == 0)
            {
                GameOver();
            }
        }
        
    }

    // Beendet das Spiel und aktiviert den GameOver-Screen
    public void GameOver()
    {
        gameIsOver = true;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    // Lädt die Szene neu, wenn man auf den Restart-Button klickt
    public void RestartTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
