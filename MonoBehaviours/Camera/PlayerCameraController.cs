using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public CinemachineInputProvider inputProvider;

    void Update()
    {
        if (AreCameraInputsAllowed())
        {
            inputProvider.enabled = true;
        }
        else {
            inputProvider.enabled = false;
        }
    }

    bool AreCameraInputsAllowed()
    {
        bool allowed = true;
        if(PixelCrushers.DialogueSystem.DialogueManager.isConversationActive)
        {
            allowed = false;
        }

        if(Time.timeScale == 0)
        {
            allowed = false;
        }

        return allowed;
    }
}
