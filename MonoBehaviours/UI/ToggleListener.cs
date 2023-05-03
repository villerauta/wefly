using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleListener : MonoBehaviour
{

    public UnityEvent OnToggleTrue = new UnityEvent();
    public void Toggled(bool p_value)
    {
        if (p_value)
        {
            OnToggleTrue.Invoke();
        }
    }
        
    
}
