using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class VolumeControl : MonoBehaviour
{
    //Audio Parameter
    [SerializeField] public GameObject mixer;

    //Array Analyze Parameter
    private List<string> musicTypeStrings = new List<string>();
    private string VolumeName = string.Empty;

    void Start()
    {
        musicTypeStrings.Clear();
        musicTypeStrings.Add("Master");
        musicTypeStrings.Add("Sound");
        musicTypeStrings.Add("Music");

        SetVolume("Master",0.5f);

    }

    public void AnalyzeArray(byte[] _byte)
    {
        Debug.Log("AnalyzeArray function");
        string inputString = System.Text.Encoding.UTF8.GetString(_byte);

        foreach (string type in musicTypeStrings)
        {
            if (inputString.Contains(type))
            {
                Debug.Log("Found substring: " + type);
                VolumeName = type;
                break;
            }
        }
    }

    public void SetVolumeFromInput(string controlInput, string UpDownInput, int volumeInput)
    {
        // controlInput need to be 'Master','Sound' or 'Music'
        // UpDownInput need to be 'increase' or 'decrease'
        //volumeInput need to be 1-100

        //check Increase or Decrease
        float x = 0;
        
        if (UpDownInput =="increase")
        {
            x = 1.0f;
        }
        else if (UpDownInput =="decrease")
        {
            x = -1.0f;
        }
        else
        {
            Debug.Log("didn't find");
        }
        
        x = x * volumeInput;
        Debug.Log("x = " + x);


    }

    public void SetVolume(string volumeName, float volumePercent)
    {
        // limited volume to 80%
        float clampedVolume = Mathf.Clamp(volumePercent, 0f, 0.8f);

        float dBValue = Mathf.Log10(clampedVolume) * 20;
        mixer.GetComponent<AudioMix>().ChangePercentageVolume(volumeName, Mathf.Log10(clampedVolume) * 20);
    }
}
