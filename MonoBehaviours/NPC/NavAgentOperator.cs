using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentOperator : MonoBehaviour
{

    public NavMeshAgent agent;
    public Animator animator;

    void FixedUpdate()
    {
        if (!animator) return;
        if (agent.enabled) animator.SetFloat("speed", agent.velocity.magnitude);
        
    }

}
