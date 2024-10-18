using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMix : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    //For Normal UI
    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVolume",Mathf.Log10(level)*20f);
    }

    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("SoundFXVolume", Mathf.Log10(level) * 20f);
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20f);
    }

    //For Input
    public void ChangePercentageVolume(string VolumeName,float level)
    {
        float percent = level/100;

        audioMixer.GetFloat(VolumeName, out float currentVolumeDB);

        float currentVolumeLinear = Mathf.Pow(10, currentVolumeDB / 20f);

        float newVolumeLinear = currentVolumeLinear * (1 + percent);
        newVolumeLinear = Mathf.Clamp(newVolumeLinear, 0.0001f, 1f);

        float newVolumeDB = 20f * Mathf.Log10(newVolumeLinear);

        Debug.Log(newVolumeDB);
        audioMixer.SetFloat(VolumeName, newVolumeDB);
    }
}
