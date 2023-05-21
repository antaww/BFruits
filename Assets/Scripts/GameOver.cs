using UnityEngine;

public class GameOver : MonoBehaviour
{
    private static GameObject _gameOverMenu;
    
    private void Awake()
    {
        _gameOverMenu = GameObject.Find("GameOver");
    }

    public static void GameOverMenu()
    {
        _gameOverMenu.SetActive(true);
    }
}