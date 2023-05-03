using System.Collections;
using System.Reflection;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModShopUI : MonoBehaviour
{
    Inventory inventory;
    List<PlanePart> planeParts;
    Toggle[] pagesList = new Toggle[0];

    public GameObject planePlatform;

    public ToggleGroup pagesParent;
    public ToggleGroup slotsParent;
    public ModShopPartSlot[] slots;

    public AirplaneAssembler assembler;
    public bool _weaponSilosActvie = false;

    //irplaneAssembler assembler;
    // Start is called before the first frame update

    void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        assembler = FindObjectOfType<AirplaneAssembler>();
    }

    void Start()
    {
        planeParts = inventory.planeParts;
        slots = slotsParent.GetComponentsInChildren<ModShopPartSlot>();

        foreach (ModShopPartSlot slot in slots)
        {
            slot.gameObject.SetActive(false);
        }

        pagesList = pagesParent.GetComponentsInChildren<Toggle>();
        for (int i = 0; i < pagesList.Length; i++)
        {
            ModShopPage page = pagesList[i].GetComponent<ModShopPage>();
            if (page.partType == "PlaneWeapon")
            {
                pagesList[i].enabled = inventory.HasWeapons();
            }

            if (page.partType == "Special")
            {
                pagesList[i].enabled = inventory.HasPlaneSpecial();
            }


            Toggle toggle = pagesList[i];
            if (i == 0)
            {
                toggle.isOn = true;
                toggle.Select();
                populateSlots(toggle.GetComponent<ModShopPage>().partType);
            }


            toggle.onValueChanged.AddListener(delegate
            {
                pageValueChanged(toggle);
            }

            );
        }
    }

    void populateSlots(string className)
    {
        foreach (ModShopPartSlot slot in slots)
        {
            slot.gameObject.SetActive(false);
        }

        foreach (PlanePart part in planeParts)
        {
            if (part.GetType().ToString() == className)
            {
                addToSlot(part);
            }
        }
    }

    void pageValueChanged(Toggle toggle)
    {
        ModShopPage page = toggle.GetComponent<ModShopPage>();
        if (toggle.isOn)
        {
            toggle.Select();
        }
        populateSlots(page.partType);
    }

    void addToSlot(PlanePart part)
    {
        foreach (ModShopPartSlot slot in slots)
        {
            if (!slot.gameObject.activeSelf)
            {
                // Check if part is active
                if (inventory.isActive(part))
                {
                    slot.GetComponent<Toggle>().SetIsOnWithoutNotify(true);
                }
                else
                {
                    slot.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
                }
                slot.gameObject.SetActive(true);
                slot.part = part;
                slot.textField_partName.text = part.name;
                slot.textField_description.text = part.description;
                return;
            }
        }
        Debug.Log("Slots are full!");
    }

    public void partSelected(PlanePart part)
    {
        assembler.activateItem(part);
        assembler.TeleportPlaneToTransform(planePlatform.transform);

    }
    public void partDeselected(PlanePart part)
    {
        assembler.deactivateItem(part);
        assembler.TeleportPlaneToTransform(planePlatform.transform);
    }

    public void SetActiveWeaponSilos(bool value)
    {
        if (!inventory.HasWeapons()) value = false;

        if (pagesList.Length == 0) return;
        foreach (Toggle toggle in pagesList)
        {
            ModShopPage page = toggle.GetComponent<ModShopPage>();
            if (page.partType == "PlaneWeapon")
            {
                toggle.enabled = value;
            }
        }
    }
}
