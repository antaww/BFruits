using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public bool isGameRunning;
    public int score;
	public int scoreDeath;
    public int lives = 3;
    public int difficulty;
    public static bool IsPaused;

    public GameObject scoreGUI;
	public GameObject scoreDeathGUI;
    public GameObject livesGUI;
	public AudioSource LivesSound;
    public GameObject pauseMenu;
	public GameObject gameOverMenu;

    public GameObject slashSound;
    public GameObject bombSound;

    public GameObject floatingCanvasPrefab;
    
    private void Awake()
    {
        difficulty = PlayerPrefs.GetInt("difficulty");
        isGameRunning = true;
        pauseMenu.SetActive(false);
		gameOverMenu.SetActive(false);
        UpdateScoreGUI();
        UpdateLivesGUI();
    }

    private void Update()
    {
        UpdateScoreGUI();
        UpdateLivesGUI();

        if (!isGameRunning) return;
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
        if (!isGameRunning) return;
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
		scoreDeathGUI.GetComponent<TextMeshProUGUI>().text = score.ToString();
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
    }

    public void RemoveLife()
    {
        if (!isGameRunning) return;
		lives--;
		LivesSound.GetComponent<AudioSource>().time = 0.3f;
		LivesSound.GetComponent<AudioSource>().volume = 0.5f;
		LivesSound.GetComponent<AudioSource>().Play();
        if (lives > 0) return;
        EndGame();
    }

    public void EndGame()
    {
        isGameRunning = false;
        gameOverMenu.SetActive(true);
    }
}