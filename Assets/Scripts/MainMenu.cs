using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        
        switch (gameObject.name)
        {
            case "Easy":
                PlayerPrefs.SetInt("difficulty", 1);
                break;
            case "Medium":
                PlayerPrefs.SetInt("difficulty", 2);
                break;
            case "Hardcore":
                PlayerPrefs.SetInt("difficulty", 3);
                break;
        }
    }
}