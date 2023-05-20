using UnityEngine;

public class Pause : MonoBehaviour
{
    public static GameObject PauseMenu;
    
    public static void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameManager.IsPaused = !GameManager.IsPaused;
    }
    
    public static void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameManager.IsPaused = !GameManager.IsPaused;
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
