using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightAttitudeDial : FlyingDial
{
    public RectTransform pitchTransform; 
    WeFly.Airplane_Characteristics planeChar;

    public override void SetValue(float p_value)
    {
        Vector3 rot = pitchTransform.localEulerAngles;
        rot.z = p_value;
        pitchTransform.localEulerAngles = rot;
    }


}
