using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathPanelTextHandler : MonoBehaviour
{
    public Text deathPanelText = null;
    // Start is called before the first frame update
    public void setDeathText(string reason)
    {
        deathPanelText.text = "Alfonso " + reason;
    }
}
