using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider _volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("Volume"))
            PlayerPrefs.SetFloat("Volume", 1f);
    }
    
    public void SetVolume()
    {
        AudioListener.volume = _volumeSlider.value;
        SaveVolume();
    }
    
    public void LoadVolume()
    {
        _volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
    }
    
    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("Volume", _volumeSlider.value);
    }
}
