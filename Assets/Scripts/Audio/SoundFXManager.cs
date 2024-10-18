using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    //store in the memory as the only one copyright
    public static SoundFXManager instance { get; private set; }

    [SerializeField]private AudioSource soundFXObject;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        
    }

    public void PlaySound(AudioClip _sound)
    {
        AudioSource source = Instantiate(soundFXObject, new Vector3(0,0,0),Quaternion.identity);
        
        source.clip = _sound;
        source.volume = 1;
        source.Play();
        float clipLength = source.clip.length;
        Destroy(source.gameObject,clipLength);

    }
}
