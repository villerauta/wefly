using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeFly;

public class MusicAreaController : MonoBehaviour
{
    ControllerManager controllerManager;
    bool mPlaneBoarded = false;

    private MusicManager musicManager;

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
            controllerManager.OnPlaneBoarded.AddListener(OnPlaneUnboarded);

            areaDetector.areasDetectedEvent.AddListener(SetAreas);
        }
        else
        {
            Debug.LogError("No area settings detector found!");
        }

    }

    public void SetAreas(Collider[] colliders)
    {
        MusicArea highestPrioArea = null;
        int highestPrio = 0;
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
                if (musicArea.priority > highestPrio)
                {
                    highestPrioArea = musicArea;
                }
            }

        }

        if (highestPrioArea)
        {
            if (mCurrentMusicArea != highestPrioArea)
            {
                mCurrentMusicArea = highestPrioArea;
                musicManager.SetMusic(mCurrentMusicArea.music, mCurrentMusicArea.priority);
            }
        }

    }

    void OnPlaneBoarded()
    {
        mCurrentMusicArea = null;
        musicManager.SetMusic(null);
        mPlaneBoarded = true;
    }

    void OnPlaneUnboarded()
    {
        mPlaneBoarded = false;
    }
}
