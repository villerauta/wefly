using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAudio : BaseAirplaneAudio
{

    public float minIdleVolume, maxIdleVolume;

    void Start()
    {
        audioSource.volume = 0f;
    }

    public void HandleIdleAudio(float throttle)
    {
        audioSource.volume = Mathf.Lerp(maxIdleVolume, minIdleVolume, throttle);
    }
}
