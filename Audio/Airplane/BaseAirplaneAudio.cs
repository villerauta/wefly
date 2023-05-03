using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class BaseAirplaneAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public bool fadeOut, oneShot;
    public float fadeOutTime;

    // Start is called before the first frame update
    void Start()
    {
        if (!audioSource) Debug.LogError("No audiosource in base airplane audio");
    }

    public void Play()
    {
        if (oneShot)
        {
            audioSource.PlayOneShot(audioSource.clip);
            return;
        }
        else
        {
            audioSource.Play();
        }
    }

    public void Stop()
    {
        if (fadeOut)
        {
            StartCoroutine(FadeOut());
        }
        else
        {
            audioSource.Stop();
        }
    }

    public IEnumerator FadeOut() 
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / fadeOutTime;
 
            yield return null;
        }

        audioSource.Stop();
    }

}
