using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class SceneTransition : MonoBehaviour
{
    public string scene;
    public string spawn;
    // Start is called before the first frame update
    public void ChangeScene()
    {
        PixelCrushers.SaveSystem.LoadScene("Gameplay@PositionModShop");
    }


}
