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
        audioMixer.SetFloat("Master",Mathf.Log10(level)*20f);
    }

    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("Sound", Mathf.Log10(level) * 20f);
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(level) * 20f);
    }

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
