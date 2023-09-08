using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedAreaManager : MonoBehaviour
{
    List<string> mVisitedPlaces = new List<string>();
    public string currentArea = "";

    public bool TryAddArea(string areaName)
    {
        if (areaName == null ||  mVisitedPlaces.Contains(areaName))
        {
            currentArea = areaName;
            return false;
        } 
        
        mVisitedPlaces.Add(areaName);
        currentArea = areaName;
        return true;
    }
}
