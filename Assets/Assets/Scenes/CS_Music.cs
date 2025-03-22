using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Music : MonoBehaviour
{
    public AudioClip Music;
    AudioSource Source;

    void Start()
    {
        Source = GetComponent<AudioSource>();
        Source.clip = Music;
        Source.loop = true;
        Source.Play();
    }
}
