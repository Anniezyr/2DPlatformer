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
using NLP;

public class VolumeControl : MonoBehaviour
{
    //Audio Parameter
    [SerializeField] public GameObject mixer;


    public void AnalyzeArray(byte[] _byte)
    {
        Debug.Log("AnalyzeArray function");
        string inputString = System.Text.Encoding.UTF8.GetString(_byte).ToLower();

        //Example
        //inputString = "set the sound volume to 40";

        var result = NLP.NLP.AnalyzeSentence(inputString);
         
        Debug.Log($"verb: {result.verb}");
        Debug.Log($"noun: {result.noun}");
        Debug.Log($"num: {result.number}");
        Debug.Log($"bool:{result.ByPercent}");

        SetVolumeFromInput(result.verb, result.noun, result.number, result.Bypercent);
    }

    public void SetVolumeFromInput(string controlInput, string UpDownInput, int volumeInput,bool bypercent)
    {
        // controlInput need to be 'Master','Sound' or 'Music'
        // UpDownInput need to be 'increase' or 'decrease'
        //volumeInput need to be 1-100

        //check Increase or Decrease
        float x = 0;
        
        if (UpDownInput =="set")
        {
            x = 1.0f;
        }
        else if (UpDownInput =="decrease")
        {
            x = -1.0f;
        }
        
        x = x * volumeInput;
        Debug.Log("x = " + x);

        if (bypercent) 
        {
            mixer.GetComponent<AudioMix>().SetVolumeByPercentage(controlInput, volumeInput);
        }
        else
        {
            // directly decrease
        }
        
    }

}
