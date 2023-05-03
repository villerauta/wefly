using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnToAdditiveScene : MonoBehaviour
{
    public SceneChange sceneChangeAsset = null;
    public bool changeOnEnable = false;

    public LayerMask cullingMask;

    public void OnEnable()
    {
        if (changeOnEnable)
        {
            Invoke();
        }
    }

    public void Invoke()
    {
        if (sceneChangeAsset != null)
        {
            if (sceneChangeAsset.unloadThisScene)
            {
                if (!sceneChangeAsset.scenesToUnload.Contains(gameObject.scene.name))
                {
                    sceneChangeAsset.scenesToUnload.Add(gameObject.scene.name);
                }   
            }
            AdditiveSceneManager additiveSceneManager = FindObjectOfType<AdditiveSceneManager>();
            additiveSceneManager.InvokeSceneChange(sceneChangeAsset);
        }
    }
}
