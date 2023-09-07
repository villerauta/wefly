using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavAgentFollow : NavAgentOperator
{
    public Transform target;
    private float nextActionTime;
    public float updateTime = 1f;

    public void Start() 
    {
        nextActionTime = Time.time + updateTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime ) {
            nextActionTime += updateTime;
            // execute block of code here
            agent.destination = target.position;
        }
    }
}
