using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-- Audio Sources --")]
    [SerializeField] AudioSource _musicSource;
    [SerializeField] AudioSource _sfxSource;
    
    [Header("-- Audio Clips --")]
    [SerializeField] AudioClip _menuMusic;
    [SerializeField] AudioClip _gameMusic;
    [SerializeField] AudioClip _slashSound;
    [SerializeField] AudioClip _gameOverSound;
    [SerializeField] AudioClip _bombSound;

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
        _musicSource.clip = _menuMusic;
        _musicSource.Play();
    }
    
    public void PlaySlashSound(AudioClip clip)
    {
        _sfxSource.PlayOneShot(_slashSound);
    }
}
