using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMix : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume") || PlayerPrefs.HasKey("MusicVolume") ||
            PlayerPrefs.HasKey("SoundVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMasterVolume();
            SetMusicVolume();
            SetSoundFXVolume();
        }
    }
    //For Normal UI
    public void SetMasterVolume()
    {
        float level = masterSlider.value;
        audioMixer.SetFloat("Master",Mathf.Log10(level)*20f);
        PlayerPrefs.SetFloat("MasterVolume", level);
    }

    public void SetSoundFXVolume()
    {
        float level = soundSlider.value;
        audioMixer.SetFloat("Sound", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("SoundVolume", level);
    }

    public void SetMusicVolume()
    {
        float level = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("MusicVolume", level);
    }

    //Load
    private void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");

        SetMasterVolume();
        SetMusicVolume();
        SetSoundFXVolume();
    }

    //For SpeechRecognition
    // Direct set
    public void SetDirectly(string VolumeName, float level)
    {
        //check the level is reasonable

        if (level < -100f || level > 100f)
        {
            Debug.Log("Out of area：" + level);
            return;
        }

        //level :0-100, change to 0.00001-1
        float linearLevel = Mathf.Lerp(0.0001f, 1f, level / 100f);

        float volumeDB = Mathf.Log10(linearLevel) * 20f;
        audioMixer.SetFloat(VolumeName, volumeDB);
    }

    //Percentage
    public void SetVolumeByPercentage(string VolumeName,float level)
    {
        //check the level is reasonable
        
        if (level < -100f || level > 100f)
        {
            Debug.Log("Out of area：" + level);
            return;
        }
        float percent = level / 100f;

        // if current volume exists
        if(audioMixer.GetFloat(VolumeName, out float currentVolumeDB))
        {
            float currentVolumeLinear = Mathf.Pow(10, currentVolumeDB / 20f);

            //adjust new volume by percent
            float newVolumeLinear = currentVolumeLinear * (1 + percent);
            newVolumeLinear = Mathf.Clamp(newVolumeLinear, 0.0001f, 1f);

            float newVolumeDB = 20f * Mathf.Log10(newVolumeLinear);

            audioMixer.SetFloat(VolumeName, newVolumeDB);
        }
        else
        {   
            Debug.Log("Wrong volume name");
        }
    }
}
