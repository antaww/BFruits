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

    private static AudioManager _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            _instance.DestroyAudioManager();
        }
        
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void DestroyAudioManager()
    {
        _instance = null;
        Destroy(gameObject);
    }

    private void Start()
    {
        musicSource.clip = menuMusic;
        musicSource.Play();
    }
}