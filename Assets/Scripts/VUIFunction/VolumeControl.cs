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


    public void AnalyzeArray(string inputString)
    {
        Debug.Log("Analyze sentence:" + inputString);
        //Example
        //inputString = "set the sound volume to 40";

        var result = NLP.NLP.AnalyzeSentence(inputString);
         
        Debug.Log($"verb: {result.verb}");
        Debug.Log($"noun: {result.noun}");
        Debug.Log($"num: {result.number}");
        Debug.Log($"bool:{result.ByPercent}");

        SetVolumeFromInput(result.verb, result.noun, result.number, result.ByPercent);
    }

    public void SetVolumeFromInput(string UpDownInput, string controlInput, int volumeInput,bool bypercent)
    {
        //check Increase or Decrease or Set
        float x = 0;
        bool Set = false;

        switch (UpDownInput)
        {
            case "increase":
                x = 1.0f;
                break;
            case "decrease":
                x = -1.0f;
                break;
            case "set":
                // set the volume directly
                Set = true;
                break;
            default:
                Debug.Log("updownInput error");
                break;
        }

        x = x * volumeInput;
        Debug.Log("x= "+x);

        if (Set)
        {
            mixer.GetComponent<AudioMix>().SetDirectly(controlInput, volumeInput);
        }
        else // increase or decrease
        {
            if (bypercent)
            {
                mixer.GetComponent<AudioMix>().SetVolumeByPercentage(controlInput, volumeInput);
            }
            else
            {
                // directly increase/decrease the 1-100 volume
            }
        }
        
        
    }

}
