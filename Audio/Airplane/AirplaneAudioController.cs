using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneAudioController : MonoBehaviour
{
    public WeFly.BaseAirplane_Input input;
    public WeFly.Airplane_Characteristics characteristics;
    public WeFly.Airplane_Controller controller;
    public EngineCutoff engineCutoffControl;
    public InputMaster controls;

    [Header("Ignition Audios")]
    public IgniteAudio[] igniteAudios;

    [Header("Constant Engine Audios")]
    public IdleAudio[] idleAudios;
    public ThrottleAudio[] throttleAudios;
    public DiveAudio[] diveAudios;
    public OverspeedAudio[] overspeedAudios;

    [Header("Non Engine Audios")]
    public SpeedAudio[] speedAudios;

    [Header("Oneshot Audios")]
    public RevAudio[] revAudios;
    public BaseAirplaneAudio[] engineStartAudios;
    public BaseAirplaneAudio[] cutoffAudios;


    // Start is called before the first frame update
    void Start()
    {
        controls = new InputMaster();
        controls.Airplane.Enable();
        input = FindObjectOfType<WeFly.BaseAirplane_Input>();
        characteristics = FindObjectOfType<WeFly.Airplane_Characteristics>();
        engineCutoffControl = FindObjectOfType<EngineCutoff>();
        controller = FindObjectOfType<WeFly.Airplane_Controller>();

        if (engineCutoffControl)
        {
            engineCutoffControl.onEngineIgniteStart.AddListener(onEngineIgniteStart);
            engineCutoffControl.onEngineIgniteStop.AddListener(onEngineIgniteStop);
            engineCutoffControl.onEngineStart.AddListener(onEngineStart);
            engineCutoffControl.onEngineCutoff.AddListener(onEngineCutoff);
        }

        foreach (SpeedAudio speed in speedAudios)
        {
            speed.Play();
        }
    }

    void OnDisable()
    {
        if (engineCutoffControl)
        {
            engineCutoffControl.onEngineIgniteStart.RemoveListener(onEngineIgniteStart);
            engineCutoffControl.onEngineIgniteStop.RemoveListener(onEngineIgniteStop);
            engineCutoffControl.onEngineStart.RemoveListener(onEngineStart);
            engineCutoffControl.onEngineCutoff.RemoveListener(onEngineCutoff);
        }

        foreach (SpeedAudio speed in speedAudios)
        {
            speed.Stop();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (controller.boarded)
        {
            if (engineCutoffControl.engineIsOn)
            {
                HandleEngineAudio();
            }

            HandleSpeedAudio();
        }

    }

    void HandleEngineAudio()
    {
        if (controls.Airplane.Throttle.WasPerformedThisFrame())
        {
            foreach (RevAudio rev in revAudios)
            {
                rev.Play();
            }
        }

        // Send throttlevalue to throttle audios
        foreach (ThrottleAudio throttle in throttleAudios)
        {
            throttle.HandleThrottleAudio(input.StickyThrottle, 
            characteristics.DiveFactor, 
            characteristics.Overspeed);
        }

        foreach (IdleAudio idle in idleAudios)
        {
            idle.HandleIdleAudio(input.StickyThrottle);
        }

        foreach (DiveAudio dive in diveAudios)
        {
            dive.HandleDiveAudio(characteristics.DiveFactor);
        }

        foreach (OverspeedAudio overspeed in overspeedAudios)
        {
            overspeed.HandleOverspeedAudio(characteristics.Overspeed);
        }


    }

    public void HandleSpeedAudio()
    {
        foreach (SpeedAudio speed in speedAudios)
        {
            speed.HandleSpeedAudio(characteristics.normalizedSpeed);
        }
    }

    public void onEngineIgniteStart()
    {
        foreach (IgniteAudio ignite in igniteAudios)
        {
            ignite.Play();
        }
    }

    public void onEngineIgniteStop()
    {
        foreach (IgniteAudio ignite in igniteAudios)
        {
            ignite.Stop();
        }
    }

    public void onEngineStart()
    {
        foreach (BaseAirplaneAudio start in engineStartAudios)
        {
            start.Play();
        }

        foreach (ThrottleAudio throttle in throttleAudios)
        {
            throttle.Play();
        }

        foreach (IdleAudio idle in idleAudios)
        {
            idle.Play();
        }

        foreach (DiveAudio dive in diveAudios)
        {
            dive.Play();
        }

        foreach (OverspeedAudio overspeed in overspeedAudios)
        {
            overspeed.Play();
        }
    }

    public void onEngineCutoff()
    {
        foreach (BaseAirplaneAudio cutoff in cutoffAudios)
        {
            cutoff.Play();
        }

        foreach (ThrottleAudio throttle in throttleAudios)
        {
            throttle.Stop();
        }

        foreach (IdleAudio idle in idleAudios)
        {
            idle.Stop();
        }

        foreach (DiveAudio dive in diveAudios)
        {
            dive.Stop();
        }

        foreach (OverspeedAudio overspeed in overspeedAudios)
        {
            overspeed.Stop();
        }


    }
}
