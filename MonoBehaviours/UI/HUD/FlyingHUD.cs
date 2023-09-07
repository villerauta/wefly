using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyingHUD : MonoBehaviour
{

    public WeFly.Airplane_Characteristics characteristics;
    public Inventory inventory;
    public EngineCutoff engineCutoff;
    public FlyingDial speedDial;
    public FlyingDial fuelDial;
    public FlyingDial altitudeDial;
    public FlightAttitudeDial attitudeDial;
    public FlightStateText stateText;
    CanvasGroup canvasGroup;
    public float transitionTime = 0.5f;

    void Start()
    {
        stateText.SetEmphasis(0);
        stateText.SetText("Engines Off");
        canvasGroup = GetComponent<CanvasGroup>();
        engineCutoff = GlobalReferences.references.Engines;

        engineCutoff.onEngineCutoff.AddListener(OnEngineStop);
        engineCutoff.onEngineStart.AddListener(OnEngineStart);
        engineCutoff.onEngineIgniteStart.AddListener(OnIgnitionStart);
        engineCutoff.onEngineIgniteStop.AddListener(OnIgnitionStop);

        if (engineCutoff.engineIsOn)
        {
            OnEngineStart();
        }

        UpdateFuel();  
    }

    public void Open()
    {
        if (gameObject.activeInHierarchy) return;

        gameObject.SetActive(true);

        canvasGroup.alpha = 0f;
        StartCoroutine(OpenCloseCoroutine(true));
    }

    public void Close()
    {
        if (!gameObject.activeInHierarchy) return;
        StartCoroutine(OpenCloseCoroutine(false));
    }

    public IEnumerator OpenCloseCoroutine(bool open)
    {

        float target  = 0f;

        if (open) target = 1f;

        Tween tween = DOTween.To(()=> canvasGroup.alpha, x=> canvasGroup.alpha = x, target, transitionTime);

        while (tween.IsPlaying())
        {
            yield return null;
        }

        if (!open)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnEngineStart()
    {
        speedDial.TurnOn();
        fuelDial.TurnOn();
        altitudeDial.TurnOn();
        attitudeDial.TurnOn();
        stateText.SetEmphasis(2);
        stateText.SetText("Engines On");
    }

    public void OnEngineStop()
    {
        speedDial.TurnOff();
        fuelDial.TurnOff();
        altitudeDial.TurnOff();
        attitudeDial.TurnOff();
        stateText.SetEmphasis(0);
        stateText.SetText("Engines Off");
    }

    public void OnIgnitionStart()
    {
        stateText.SetEmphasis(1);
        stateText.SetText("Ignition on");
    }

    public void OnIgnitionStop()
    {
        if (!engineCutoff.engineIsOn)
        {
            stateText.SetEmphasis(0);
            stateText.SetText("Engines off");
        }
    }


    void Update()
    {
        if (engineCutoff.engineIsOn)
        {
            UpdateSpeed();
            UpdateAltitude();
            UpdateFuel();
            UpdateAttitude();
        }
    }

    void UpdateSpeed()
    {
        speedDial.SetValue(characteristics.forwardSpeed);
    }

    void UpdateAltitude()
    {
        altitudeDial.SetValue(characteristics.transform.position.y);
    }

    void UpdateFuel()
    {
        fuelDial.SetValue(inventory.fuelAmount/inventory.maxFuel);
    }

    void UpdateAttitude()
    {
        attitudeDial.SetValue(characteristics.transform.localEulerAngles.z);
    }
}
