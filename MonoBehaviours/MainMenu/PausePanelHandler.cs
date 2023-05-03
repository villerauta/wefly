using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanelHandler : MonoBehaviour
{
    public Button exitRaceButton;
    public Button saveButton;

    // Start is called before the first frame update
    void OnEnable()
    {
        GameStateManager gameState = FindObjectOfType<GameStateManager>();
        if (!gameState) return;
        
        saveButton.gameObject.SetActive(!gameState.getInRace());
        exitRaceButton.gameObject.SetActive(gameState.getInRace());
    }


}
