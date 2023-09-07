using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirportAreaController : MonoBehaviour
{
    // Start is called before the first frame update
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
                GlobalReferences.references.airportManager.SetAirportArea(airfield);
            }
        }
    }
}
