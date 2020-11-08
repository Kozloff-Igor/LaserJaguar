using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    public AudioClip[] clips;

    AudioSource mySource;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            mySource = GetComponent<AudioSource>();
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }


    }

    public void StartClip(int index)
    {
        mySource.clip = clips[index];
        mySource.Play();
    }
}
