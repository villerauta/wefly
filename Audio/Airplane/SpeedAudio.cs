using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedAudio : BaseAirplaneAudio
{
    public float minSpeedVolume, maxSpeedVolume, minSpeedPitch, maxSpeedPitch;

    public void HandleSpeedAudio(float normalizedSpeed)
    {
        audioSource.volume = Mathf.Lerp(minSpeedVolume, maxSpeedVolume, normalizedSpeed);
        audioSource.pitch = Mathf.Lerp(minSpeedPitch, maxSpeedPitch, normalizedSpeed);
    }
}
