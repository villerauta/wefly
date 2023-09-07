using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicArea : MonoBehaviour
{
    public AudioClip music = null;
    public int priority = 1;
    public SphereCollider sphereCollider;
    AudioClip Music { get; }
    int Priority { get; }

}
