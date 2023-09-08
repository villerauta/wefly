using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirportManager : MonoBehaviour
{
    public AirfieldArea CurrentAirportArea;

    public void SetAirportArea(AirfieldArea area)
    {
        if (area != CurrentAirportArea)
        {
            CurrentAirportArea = area;
        }
    }
}
