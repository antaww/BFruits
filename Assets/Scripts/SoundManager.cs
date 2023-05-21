using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    // Start is called before the first frame update

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Volume")) PlayerPrefs.SetFloat("Volume", 1f);
    }


    public void SetVolume()
    {
        AudioListener.volume = volumeSlider.value;
        SaveVolume();
    }

    public void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }
}