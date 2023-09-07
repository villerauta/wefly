using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DistantLands.Cozy;

public class House : MonoBehaviour
{
    // public void OnDrawGizmosSelected() for performance
    public void OnDrawGizmosSelected()
    {
        if (insideLightsGameobjects.Length != 0)
        {
            foreach (GameObject light in insideLightsGameobjects)
            {
                if (!light) return;
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(light.transform.position, 0.3f);
            }
        }


    }


    public CozyEventManager eventManager = null;
    public CozyWeather.CozyCalendar calendar = null;

    public float awake = 70f;
    public float sleep = 200f;
    public GameObject[] insideLightsGameobjects = null;
    public GameObject actionOnAwake = null;
    public GameObject actionOnSleep = null;

    private bool lightsOn = true;

    void OnEnable()
    {
        CozyWeather.Events.onNewTick += CheckTime;
        /*
        eventManager = FindObjectOfType<CozyEventManager>();
        if (eventManager)
        {
            eventManager.onNewTick.AddListener(CheckTime);
        }
        */

        calendar = CozyWeather.instance.calendar;
    }

    void OnDisable()
    {
        CozyWeather.Events.onNewTick -= CheckTime;
        /*if (eventManager)
        {
            eventManager.onNewTick.RemoveListener(CheckTime);
        }
        */
    }

    // Sleep 23
    // Awake 6

    // current 1

    void CheckTime()
    {

        //Debug.Log(calendar.GetCurrentTick() + " tick");
        if (calendar.currentTicks < sleep && calendar.currentTicks > awake)
        {
            if (lightsOn) return;
            // Lights on
            foreach (GameObject light in insideLightsGameobjects)
            {
                light.SetActive(true);
            }

            if (actionOnAwake) actionOnAwake.SetActive(true);
            if (actionOnSleep) actionOnSleep.SetActive(false);

            lightsOn = true;

        }
        else
        {
            if (!lightsOn) return;
            // Lights out
            foreach (GameObject light in insideLightsGameobjects)
            {
                light.SetActive(false);
            }

            if (actionOnAwake) actionOnAwake.SetActive(false);
            if (actionOnSleep) actionOnSleep.SetActive(true);

            lightsOn = false;
        }

    }

}

