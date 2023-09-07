using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public AudioSource MusicSource;

    [SerializeField]
    private AudioClip mCurrentClip;
    [SerializeField]
    private int mCurrentPriority = 0;
    public void SetMusic(AudioClip clip, int priority = 1)
    {
        if (clip)
        {
            if (priority > mCurrentPriority)
            {
                mCurrentPriority = priority;
                ChangeMusic(clip);
            }
        } 
        else 
        {
            ResetMusic();
        }
    }

    private void ChangeMusic(AudioClip clip)
    {
        mCurrentClip = clip;
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    private void ResetMusic()
    {
        MusicSource.clip = null;
        MusicSource.Stop();
    }

}
