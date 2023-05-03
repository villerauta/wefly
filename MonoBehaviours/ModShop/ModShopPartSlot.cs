using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModShopPartSlot : MonoBehaviour
{
    public ModShopUI mainUI;
    public PlanePart part;
    public Text textField_partName;
    public Text textField_description;
    // Start is called before the first frame update
    void Start()
    {
        mainUI = FindObjectOfType<ModShopUI>();
    }

    public void partSelected(bool toggle) {
        if (toggle) {
            mainUI.partSelected(part);
        } else {
            mainUI.partDeselected(part);
        }
        
    }

}
