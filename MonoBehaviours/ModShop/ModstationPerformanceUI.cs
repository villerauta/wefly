using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModstationPerformanceUI : MonoBehaviour
{
    public Text power;
    public Text aerodynamics;
    public Text brakes;
    public Text handling;
    public Text lift;
    public Text flaps;

    public void populateSlots(PlanePerformanceData data) 
    {
        // Just numbers for now
        power.text = data.power.ToString();
        aerodynamics.text = data.aero.ToString();
        lift.text = data.lift.ToString();
        brakes.text = data.brakes.ToString();
        flaps.text = data.flaps.ToString();
        handling.text = data.handling.ToString();
    }

    
}
