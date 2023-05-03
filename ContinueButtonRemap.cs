using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButtonRemap : MonoBehaviour
{
    public Button continueButton;
    public InputMaster inputActions;

    void Start()
    {
        inputActions = new InputMaster();
        inputActions.Player.Enable();
    }

    void Update()
    {
        if (inputActions.Player.Use.WasPerformedThisFrame())
        {
            if (continueButton.enabled)
            {
                Debug.Log("Continue button invoked");
                continueButton.onClick.Invoke();
            }
            
        }
    }

}
