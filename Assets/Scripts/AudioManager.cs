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

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Instance.DestroyAudioManager();
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void DestroyAudioManager()
    {
        Instance = null;
        Destroy(gameObject);
    }

    private void Start()
    {
        musicSource.clip = menuMusic;
        musicSource.Play();
    }
}