using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FlyingDial : MonoBehaviour
{
    public TMPro.TextMeshProUGUI dial;
    public CanvasGroup container;
    public float transitionTime = 0.5f;

    void Start()
    {
        container.alpha = 0;
    }

    public virtual void SetValue(float p_value)
    {
        dial.text = p_value.ToString("F0");
    }

    public virtual void TurnOn()
    {
        if (!container) return;
        Tween tween = DOTween.To(()=> container.alpha, x=> container.alpha = x, 1, transitionTime);
    }

    public virtual void TurnOff()
    {
        if (!container) return;
        StartCoroutine(TurnOffCoroutine());
    }

    public IEnumerator TurnOffCoroutine()
    {
        Tween tween = DOTween.To(()=> container.alpha, x=> container.alpha = x, 0, transitionTime);

        while (tween.IsPlaying())
        {
            yield return null;
        }


    }
}
