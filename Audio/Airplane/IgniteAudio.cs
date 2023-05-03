using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgniteAudio : BaseAirplaneAudio
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayIgniteAudio(bool play)
    {
        if (play) 
        {
            audioSource.Play();
        }
        else 
        {
            audioSource.Stop();
        }
    }
}
