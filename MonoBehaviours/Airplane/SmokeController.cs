using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeController : MonoBehaviour
{
    private WeFly.Airplane_Characteristics characteristics;
    private ParticleSystem smokeParticles;
    private EngineCutoff cutoff;
    private WeFly.BaseAirplane_Input input;
    // Start is called before the first frame update

    public float maxThrottleEmission = 60f;
    public float minThrottleEmission = 10f;

    public float maxThrottleEmissionSpeed = 10f;
    public float minThrottleEmissionSpeed = 4f;

    public float maxThrottleEmissionSize = 10f;
    public float minThrottleEmissionSize = 2f;

    
    void Start()
    {
        characteristics = FindObjectOfType<WeFly.Airplane_Characteristics>();
        smokeParticles = GetComponent<ParticleSystem>();    
        cutoff = FindObjectOfType<EngineCutoff>();
        input = FindObjectOfType<WeFly.BaseAirplane_Input>();

        cutoff.onEngineStart.AddListener(onEngineStart);
        cutoff.onEngineCutoff.AddListener(onEngineCutoff);

        if (!cutoff.engineIsOn)
        {
            smokeParticles.Stop();
        }
        else {
            smokeParticles.Play();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (characteristics && smokeParticles && cutoff)
        {
            if (smokeParticles.isPlaying) 
            {
                // Amount
                var emission = smokeParticles.emission;
                float emissionAmount = Mathf.Lerp(minThrottleEmission,maxThrottleEmission,input.StickyThrottle);
                emission.rateOverTime = emissionAmount;

                var main = smokeParticles.main;
                
                // Speed
                float emissionSpeed = Mathf.Lerp(minThrottleEmissionSpeed,maxThrottleEmissionSpeed, input.StickyThrottle);
                main.startSpeedMultiplier = emissionSpeed;

                // Size
                float emissionSize = Mathf.Lerp(minThrottleEmissionSize, maxThrottleEmissionSize, input.StickyThrottle);
                main.startSizeMultiplier = emissionSize;

            }

        }
    }

    void onEngineStart()
    {
        smokeParticles.Play();
    }
    void onEngineCutoff()
    {
        smokeParticles.Stop();
    }
}
