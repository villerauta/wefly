using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public FlyingHUD flyingHUD;

    //Todo: Fancy animations, use FEEL?

    void Start()
    {
        flyingHUD.Close();
    }

    public void OnPlaneBoarded()
    {
        flyingHUD.Open();
    }

    public void OnPlaneOffboarded()
    {
        flyingHUD.Close();
    }

}
