using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WeFly;

public class MusicAreaController : MonoBehaviour
{
    ControllerManager controllerManager;
    bool mPlaneBoarded = false;

    private MusicManager musicManager;

    private Collider[] mCurrentAreas = new Collider[0];

    [SerializeField]
    private MusicArea mCurrentMusicArea = null;

    void Start()
    {
        SettingsAreaDetector areaDetector = GetComponent<SettingsAreaDetector>();
        if (areaDetector)
        {
            controllerManager = GlobalReferences.references.controllerManager;
            musicManager = GlobalReferences.references.musicManager;
            mPlaneBoarded = controllerManager.GetBoarded();
            controllerManager.OnPlaneBoarded.AddListener(OnPlaneBoarded);
            controllerManager.OnPlaneOffBoarded.AddListener(OnPlaneUnboarded);

            areaDetector.areasDetectedEvent.AddListener(SetAreas);
        }
        else
        {
            Debug.LogError("No area settings detector found!");
        }

    }

    public void SetAreas(Collider[] colliders)
    {
        mCurrentAreas = colliders;
        MusicArea highestPrioArea = null;

        bool currentMusicAreaInAreas = false;

        List<MusicArea> musicAreas = getMusicAreas(colliders);
        
        if (musicAreas.Count == 0) return;

        string message = "";
        foreach(MusicArea area in musicAreas)
        {
            message = message + area.name + " ";
        }
        Debug.Log(message);
        
        if (mCurrentMusicArea && musicAreas.Contains(mCurrentMusicArea)) currentMusicAreaInAreas = true;
        if(currentMusicAreaInAreas) Debug.Log("CURRENT FOUND");

        //Get highest prio area;
        foreach(MusicArea area in musicAreas)
        {
            if(!highestPrioArea || area.priority > highestPrioArea.priority )
            {
                highestPrioArea = area;
            }
        }

        if (currentMusicAreaInAreas)
        {
            if (highestPrioArea.priority > mCurrentMusicArea.priority)
            {
                setMusicArea(highestPrioArea);
            }
        }
        else
        {
            setMusicArea(highestPrioArea);
        }

    }

    private void setMusicArea(MusicArea area)
    {
        if (!area) Debug.LogError("Trying to setMusicArea with null area");
        mCurrentMusicArea = area;
        musicManager.SetMusic(area.music);
    }

    private List<MusicArea> getMusicAreas(Collider[] colliders)
    {
        List<MusicArea> musicAreas = new List<MusicArea>();
   
        foreach (Collider coll in colliders)
        {
            MusicArea musicArea = null;
            if (mPlaneBoarded)
            {
                musicArea = coll.transform.GetComponent<FlyingMusicArea>();
            }
            else
            {
                musicArea = coll.transform.GetComponent<WalkingMusicArea>();
            }

            if (musicArea)
            {
                musicAreas.Add(musicArea);
            }
        }

        return musicAreas;
    }

    void OnPlaneBoarded()
    {
        Debug.Log("PLANE BOARDED in music area controller");
        mPlaneBoarded = true;
        mCurrentMusicArea = null;
        musicManager.SetMusic(null);
        SetAreas(mCurrentAreas);
    }

    void OnPlaneUnboarded()
    {
        Debug.Log("PLANE UNBOARDED in music area controller");
        mPlaneBoarded = false;
        mCurrentMusicArea = null;
        musicManager.SetMusic(null);
        SetAreas(mCurrentAreas);
    }
}
