using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class FlightStateText : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textField;

    Color originalColor;

    public void Start()
    {
        originalColor = textField.color;
    }

    public void SetText(string text)
    {
        textField.text = text;
    }

    public void SetEmphasis(int emp)
    {
        Color color = Color.white;
        if (emp == 0)
        {
            color = Color.gray;
        }

        if (emp == 1)
        {
            color = Color.red;
        }

        if(emp == 2)
        {
            color = Color.white;
        }

        textField.DOColor(color,0.25f);
    }
}
