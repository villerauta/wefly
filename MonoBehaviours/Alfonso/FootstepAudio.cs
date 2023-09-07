using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepAudio : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] audioClips;

    public void PlayFootstep()
    {
        //ReselectClip();
        source.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
        
    }

    void ReselectClip()
    {
        //source.clip = audioClips[Random.Range(0, audioClips.Length)];
    }
}
