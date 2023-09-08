using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMusicArea : MusicArea
{
    void OnDrawGizmos()
    {
        if (!sphereCollider) return;
        Color color = Color.blue;
        color.a = 0.5f;
        color.r = color.r + (20*(priority-1));
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position,sphereCollider.radius*transform.localScale.x);
        
    }

}
