using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Backpack : MonoBehaviour
{

    private Transform uiPanel;

    public List<Slot> slots;

    private void Awake()
    {
        uiPanel = transform.Find("UI");
    }

    private void Start()
    {
        InitUI();
        UpdateUI();
    }

    private void InitUI()
    {
        slots = new List<Slot>(new Slot[24]);
        var slotsGrid = transform.Find("UI/Slots");
        for (int i = 0; i < slotsGrid.childCount; i++)
        {
            slots[i] = slotsGrid.GetChild(i).GetComponent<Slot>();
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < InventoryManager.Instance.backpack.slots.Count; i++)
        {
            slots[i].SetData(InventoryManager.Instance.backpack.slots[i]);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleUI();
        }
    }

    private void ToggleUI()
    {
        uiPanel.gameObject.SetActive(!uiPanel.gameObject.activeSelf);
    }

    public void OnCloseClick()
    {
        ToggleUI();
    }
}
