using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepDetector : MonoBehaviour
{
    public FootstepController footstepController;

    private void OnTriggerEnter(Collider other)
    {
        footstepController.PlayFootstep();
    }
    
}