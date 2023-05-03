using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitRace : MonoBehaviour
{
    public void tryExitRace()
    {
        FindObjectOfType<GameStateManager>().getActiveRace().ExitTrack();
    }
}
