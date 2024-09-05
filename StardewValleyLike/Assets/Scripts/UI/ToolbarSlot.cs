using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarSlot : Slot
{
    public Sprite unselected;
    public Sprite selected;

    private Image background;

    private void Start()
    {
        background = GetComponent<Image>();
    }

    public void Select()
    {
        background.sprite = selected;
    }

    public void Unselect()
    {
        background.sprite = unselected;
    }
}
