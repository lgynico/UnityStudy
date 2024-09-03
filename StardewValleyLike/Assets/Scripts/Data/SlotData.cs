using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SlotData
{
    public ItemData item;
    public int count;

    public bool CanAddItem()
    {
        return count < item.maxCount;
    }

    public void Increase()
    {
        count++;
    }

    public void AddItem(ItemData item)
    {
        this.item = item;
        count = 1;
    }
}
