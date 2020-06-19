using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    AudioSource speaker;
    [SerializeField]
    AudioClip clipClap;
    [SerializeField]
    AudioClip clipgoodJob;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Q))
        {
            playClap();
        }
        if(Input.GetKeyUp(KeyCode.E))
        {
            playGoodJob();
        }
    }

    void playClap()
    {
        speaker.clip = clipClap;
        speaker.Play();
    }

    void playGoodJob()
    {
        speaker.clip = clipgoodJob;
        speaker.Play();
    }
}
