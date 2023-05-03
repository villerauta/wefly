using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverspeedAudio : BaseAirplaneAudio
{
    public float minOverspeedVolume,
    maxOverspeedVolume,
    minOverspeedPitch,
    maxOverspeedPitch,
    maxOverspeed;

    public void HandleOverspeedAudio(float overspeed)
    {
        if (overspeed > 0)
        {
            float factor = Mathf.Lerp(0f, maxOverspeed, overspeed);

            audioSource.volume = Mathf.Lerp(minOverspeedVolume,maxOverspeedVolume, factor);
            audioSource.pitch = Mathf.Lerp(minOverspeedPitch, maxOverspeedPitch, factor);
        }
        else
        {
            audioSource.volume = 0f;
        }
    }
}
