using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneWheelFX : MonoBehaviour
{
    public float rollingAudioFactor = 1f;
    public AudioSource rollingSoundSource;

    public float breakingAudioFactor = 1f;
    public AudioSource breakingSoundSource;
    public ParticleSystem breakParticleSystem;

    WeFly.BaseAirplane_Input input;
    WeFly.Airplane_Characteristics characteristics;
    // Start is called before the first frame update
    void Start()
    {
        characteristics = GlobalReferences.references.PlaneCharasteristics;

        input = FindObjectOfType<WeFly.BaseAirplane_Input>();
        if (!input)
        {
            Debug.LogError("No airplane input found for wheel FX!");
        }

        if (!characteristics)
        {
            Debug.LogError("No airplane charasteristics found for wheel FX!");
        }
    }

    public void HandleBreakingFX(bool grounded)
    {
        if (input.Brake > 0 && grounded)
        {
            /* Sound */
            float volume = breakingAudioFactor * characteristics.normalizedSpeed;
            Mathf.Clamp01(volume);

            breakingSoundSource.volume = volume;
            if (!breakingSoundSource.isPlaying) breakingSoundSource.Play();
            

            /* Particles */
            if (!breakParticleSystem.isPlaying) breakParticleSystem.Play();
        }
        else
        {
            if (breakingSoundSource.isPlaying) breakingSoundSource.Stop();
            if (breakParticleSystem.isPlaying) breakParticleSystem.Stop();
        }
    }

    public void HandleRollingFX(bool grounded)
    {
        if (grounded)
        {
            float volume = rollingAudioFactor * characteristics.normalizedSpeed;
            Mathf.Clamp01(volume);

            rollingSoundSource.volume = volume;

            if (!rollingSoundSource.isPlaying) rollingSoundSource.Play();
            
        }
        else
        {
            if (rollingSoundSource.isPlaying) rollingSoundSource.Stop();
        }
    }
}
