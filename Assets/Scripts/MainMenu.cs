using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame(string name)
    {
        SceneManager.LoadScene(name);
    }
}