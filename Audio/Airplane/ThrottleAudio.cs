using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrottleAudio : BaseAirplaneAudio
{

    public float minThrottleVolume,
    maxThrottleVolume,
    minThrottlePitch,
    maxThrottlePitch;

    public float divePitch = 0f;
    public float overspeedPitch = 0f;

    private float lastDiveFactor = 0f;

    void Start()
    {
        audioSource.volume = 0f;
    }

    public void HandleThrottleAudio(float throttle, float diveFactor, float overspeed)
    {
        float divePitchAmount = divePitch * diveFactor;

        float overspeedAmount = overspeedPitch * Mathf.InverseLerp(0f, 5f, overspeed);
        
        audioSource.volume = Mathf.Lerp(minThrottleVolume,maxThrottleVolume,throttle);

        audioSource.pitch = Mathf.Lerp(minThrottlePitch, maxThrottlePitch, throttle)
        + divePitchAmount
        + overspeedAmount;
    }

}
