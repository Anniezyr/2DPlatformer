using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{

    [Header("InputField Parameters")]
    [SerializeField] private TMP_InputField controlInput;
    [SerializeField] private TMP_InputField UpDownInput;
    [SerializeField] private TMP_InputField volumeInput;

    [SerializeField] private GameObject mixer;

    void Start()
    {
        controlInput.text = "Enter'MasterVolume','SoundFXVolume' or 'MusicVolume'";
        UpDownInput.text = "Enter 'increase' or 'decrease'";
        volumeInput.text = "Enter 1 number between 1-100";

        volumeInput.onEndEdit.AddListener(SetVolumeFromInput);
        
    }

    public void SetVolumeFromInput(string input)
    {
        //change string to int
        int changeVolume = 0;
        try
        {
            changeVolume = int.Parse(input.Trim());
            Debug.Log("Volume = "+ changeVolume);
        }
        catch (System.FormatException)
        {
            Debug.LogError("error1:transform failed");
        }
        catch (System.OverflowException)
        {
            Debug.LogError("error2:overscope");
        }


        //check Increase or Decrease
        float x = 0;
        
        if (UpDownInput.text.Equals("increase", System.StringComparison.OrdinalIgnoreCase))
        {
            x = 1.0f;
        }
        else if (UpDownInput.text.Equals("decrease", System.StringComparison.OrdinalIgnoreCase))
        {
            x = -1.0f;
        }
        else
        {
            Debug.Log("«Î ‰»Î 'increase' ªÚ 'decrease'");
        }
        
        x = x * changeVolume;
        Debug.Log("x = " + x);

        mixer.GetComponent<AudioMix>().ChangePercentageVolume(controlInput.text,x);

        volumeInput.text = "";
        controlInput.text = "";
        UpDownInput.text = "";
    }
}
