using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PixelCrushers.DialogueSystem;

public class WeFlySubtitleUIPanel : StandardUISubtitlePanel
{
    //public UnityUITextFieldUI nameText;
    public override void SetContent(Subtitle subtitle)
    {
        foreach (UIFollow uIFollow in GetComponents<UIFollow>())
        {
            uIFollow.followThis = subtitle.speakerInfo.transform;
        }
        //GetComponent<UIFollow>().followThis = subtitle.speakerInfo.transform;
        //nameText.textField.text = subtitle.speakerInfo.Name;
        base.SetContent(subtitle);
    }
}
