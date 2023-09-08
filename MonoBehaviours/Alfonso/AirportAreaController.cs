using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirportAreaController : MonoBehaviour
{
    [SerializeField]
    private AirfieldArea currentAirfield = null;
    void Start()
    {
        SettingsAreaDetector areaDetector = GetComponent<SettingsAreaDetector>();
        areaDetector.areasDetectedEvent.AddListener(SetAreas);
    }

    void SetAreas(Collider[] areas)
    {
        foreach(Collider area in areas)
        {
            AirfieldArea airfield = area.transform.GetComponent<AirfieldArea>();
            if (airfield)
            {
                if (!currentAirfield || airfield != currentAirfield)
                {
                    currentAirfield = airfield;
                    GlobalReferences.references.airportManager.SetAirportArea(airfield);
                }
                
            }
        }
    }
}
