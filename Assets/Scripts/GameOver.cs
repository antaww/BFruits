using UnityEngine;

public class GameOver : MonoBehaviour
{
    private static GameObject _GameOverMenu;
    
    private void Awake()
    {
        _GameOverMenu = GameObject.Find("GameOver");
    }

    public static void GameOverMenu()
    {
        _GameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}