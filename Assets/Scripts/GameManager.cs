using System.Collections;
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
    
    //Bonuses
    public bool isDoublePoints;
    public bool isTriplePoints;

    public GameObject scoreGUI;
    public GameObject scoreDeathGUI;
    public GameObject livesGUI;
    public AudioSource LivesSound;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
	public GameObject Multiplicator;

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
		Multiplicator.GetComponent<TextMeshProUGUI>().text = "x" + (isTriplePoints ? 3 : isDoublePoints ? 2 : 1);
		//Set color of multiplicator to white if 1 to green if 2 to yellow if 3
		Multiplicator.GetComponent<TextMeshProUGUI>().color = isTriplePoints ? new Color32(255, 255, 0, 255) : isDoublePoints ? new Color32(0, 255, 0, 255) : new Color32(255, 255, 255, 255);
    }

    public void AddScore(int points, bool isBomb)
    {
        if (!isGameRunning) return;
        // Score GUI animation
        LeanTween.scale(scoreGUI, new Vector3(1.1f, 1.1f, 1.1f), 0.3f).setEasePunch();
        LeanTween.scale(scoreGUI.transform.parent.gameObject, new Vector3(1.1f, 1.1f, 1.1f), 0.3f).setEasePunch();

        score += points * (isTriplePoints ? 3 : isDoublePoints ? 2 : 1);

        if (isBomb) return;
        slashSound.GetComponent<AudioSource>().time = 1.2f;
        slashSound.GetComponent<AudioSource>().volume = 2f;
        slashSound.GetComponent<AudioSource>().Play();
    }
    
    public void RemoveScore(int points)
    {
        if (!isGameRunning) return;
        // Score GUI animation
        LeanTween.scale(scoreGUI, new Vector3(1.1f, 1.1f, 1.1f), 0.3f).setEasePunch();
        LeanTween.scale(scoreGUI.transform.parent.gameObject, new Vector3(1.1f, 1.1f, 1.1f), 0.3f).setEasePunch();

        score -= points;

        slashSound.GetComponent<AudioSource>().time = 1.2f;
        slashSound.GetComponent<AudioSource>().volume = 2f;
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
        LivesSound.GetComponent<AudioSource>().Play();
        if (lives > 0) return;
        EndGame();
    }

    public void EndGame()
    {
        isGameRunning = false;
        gameOverMenu.SetActive(true);
    }

    public void ShowFloatingText(string effect, Vector3 position, byte r, byte g, byte b)
    {
        var instantiatedFloatingText = Instantiate(floatingCanvasPrefab, new Vector3(position.x, position.y, -42.6f), Quaternion.identity);
        instantiatedFloatingText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color32(r, g, b, 255);
        instantiatedFloatingText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = effect;
        LeanTween.scale(instantiatedFloatingText, new Vector3(1.1f, 1.1f, 1.1f), 0.3f).setEasePunch();
        LeanTween.scale(instantiatedFloatingText, new Vector3(1f, 1f, 1f), 0.3f).setEasePunch().setDelay(0.3f);
        LeanTween.scale(instantiatedFloatingText, new Vector3(0f, 0f, 0f), 0.3f).setDelay(0.9f).setOnComplete(() => Destroy(instantiatedFloatingText));
    }

    public void DisableDoublePoints(float x2Timer)
    {
        StartCoroutine(DisableCoroutineDoublePoints(x2Timer));
    }
    
    private IEnumerator DisableCoroutineDoublePoints(float x2Timer)
    {
        isDoublePoints = true;
        yield return new WaitForSeconds(x2Timer);
        isDoublePoints = false;
    }
    
    public void DisableTriplePoints(float x3Timer)
    {
        StartCoroutine(DisableCoroutineTriplePoints(x3Timer));
    }
    
    private IEnumerator DisableCoroutineTriplePoints(float x3Timer)
    {
        isTriplePoints = true;
        yield return new WaitForSeconds(x3Timer);
        isTriplePoints = false;
    }
}