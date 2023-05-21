using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    [FormerlySerializedAs("_musicSource")] [Header("-- Audio Sources --")] [SerializeField]
    private AudioSource musicSource;

    [SerializeField] private AudioSource sfxSource;

    [Header("-- Audio Clips --")] [SerializeField]
    private AudioClip menuMusic;

    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private AudioClip slashSound;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip bombSound;

    private void Awake()
    {
        //if scene name is menu, play menu music
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SampleScene")
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        musicSource.clip = menuMusic;
        musicSource.Play();
    }
}