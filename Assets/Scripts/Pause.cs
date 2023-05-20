using UnityEngine;

public class Pause : MonoBehaviour
{
    private static GameObject _pauseMenu;
    
    private void Awake()
    {
        _pauseMenu = GameObject.Find("PauseMenu");
    }

    public static void PauseGame()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameManager.IsPaused = !GameManager.IsPaused;
    }
    
    public static void ResumeGame()
    {
        print("ResumeGame");
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameManager.IsPaused = !GameManager.IsPaused;
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
