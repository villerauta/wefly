using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VerticalFuelDial : FlyingDial
{
    public bool isActuallyHorizontal = true;
    public RectTransform fillTransform;

    public override void SetValue(float p_value)
    {
        base.SetValue(p_value*100);

        Vector3 scale = fillTransform.localScale;
        if (isActuallyHorizontal)
        {
            scale.x = p_value;
        }
        else{
            scale.y = p_value;
        }
        
        fillTransform.localScale = scale;
    }
}
