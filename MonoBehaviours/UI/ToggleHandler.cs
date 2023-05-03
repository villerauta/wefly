using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleHandler : MonoBehaviour
{

    public UnityEngine.UI.Toggle myToggle;

    public UnityEvent OnToggleOn = new UnityEvent();
    public UnityEvent OnToggleOff = new UnityEvent();

    public void OnToggleChanged()
    {
        if (myToggle.isOn)
        {
            OnToggleOn.Invoke();
        }
        else
        {
            OnToggleOff.Invoke();
        }
    }
}
