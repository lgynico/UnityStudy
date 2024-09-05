using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar : MonoBehaviour
{
    public List<ToolbarSlot> slots;

    private ToolbarSlot selected;


    private void Start()
    {
        InitUI();
        UpdateUI();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            for (var i = KeyCode.Alpha1; i <= KeyCode.Alpha9; i++)
            {
                if (Input.GetKeyDown(i))
                {
                    selected?.Unselect();

                    int index = i - KeyCode.Alpha1;
                    slots[index].Select();
                    selected = slots[index];
                }
            }
        }
    }

    private void InitUI()
    {
        slots = new List<ToolbarSlot>(new ToolbarSlot[9]);
        var slotsGrid = transform.Find("Slots");
        for (int i = 0; i < slotsGrid.childCount; i++)
        {
            slots[i] = slotsGrid.GetChild(i).GetComponent<ToolbarSlot>();
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < InventoryManager.Instance.toolbar.slots.Count; i++)
        {
            slots[i].SetData(InventoryManager.Instance.toolbar.slots[i]);
        }
    }
}
