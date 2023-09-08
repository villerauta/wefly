using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedAreaController : MonoBehaviour
{
    [SerializeField]
    private NamedArea mCurrentArea = null;

    //Todo: UnityEvent OnNamedAreaChanged

    void Start()
    {
        SettingsAreaDetector areaDetector = GetComponent<SettingsAreaDetector>();
        areaDetector.areasDetectedEvent.AddListener(SetAreas);
    }

    void SetAreas(Collider[] areas)
    {
        bool areaFound = false;
        NamedArea highestPrioArea = null;
        foreach (Collider area in areas)
        {
            NamedArea namedArea = area.transform.GetComponent<NamedArea>();
            if (namedArea)
            {
                areaFound = true;
                if (!highestPrioArea || namedArea.Priority > highestPrioArea.Priority)
                {
                    highestPrioArea = namedArea;
                }

            }
        }

        if (!areaFound)
        {
            mCurrentArea = null;
            GlobalReferences.references.namedAreaManager.TryAddArea(null);
            return;
        } 

        if (!mCurrentArea || highestPrioArea.AreaName != mCurrentArea.AreaName)
        {
            mCurrentArea = highestPrioArea;
            bool newArea = GlobalReferences.references.namedAreaManager.TryAddArea(highestPrioArea.AreaName);
            if (newArea)
            {
                Debug.Log("NEW AREA");
            }
        }
    }

}
