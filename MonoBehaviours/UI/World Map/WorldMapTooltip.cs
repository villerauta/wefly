using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class WorldMapTooltip : MonoBehaviour
{

    public RectTransform rect;
    public TMPro.TextMeshProUGUI textField;
    

    Tween currentAction;


    public void Show(string description)
    {

        SetDescription(description);

        if (gameObject.activeInHierarchy) return;
        
        rect.localScale = Vector3.zero;
        gameObject.SetActive(true);
        rect.DOScale(Vector3.one,0.1f).SetUpdate(true).SetEase(Ease.InCirc);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        return;
        print("Hide");
        StartCoroutine(HideCoroutine());
    }

    IEnumerator HideCoroutine()
    {
        Tween hideTween = rect.DOScale(Vector3.zero,0.1f).SetUpdate(true).SetEase(Ease.OutCirc);
        yield return hideTween.WaitForCompletion();

        gameObject.SetActive(false);
    }

    public bool IsHidden()
    {
        return !gameObject.activeInHierarchy;
    }

    void SetDescription(string desc)
    {
        textField.SetText(desc);
    }
}
