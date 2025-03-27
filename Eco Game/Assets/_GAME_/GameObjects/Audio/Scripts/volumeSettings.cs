using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class volumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            loadMusicVolume();
        }
        else
        {
            setMusicVolume();
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            loadSFXVolume();
        }
        else
        {
            setSFXVolume();
        }

    }

    public void setMusicVolume ()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void setSFXVolume ()
    {
        float volume = SFXSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void loadMusicVolume ()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void loadSFXVolume()
    {
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }
}
