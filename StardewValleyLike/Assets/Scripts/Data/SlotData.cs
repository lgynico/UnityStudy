using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SlotData
{
    public ItemData item;
    public int count;
    private Action onChange;

    public bool CanAddItem()
    {
        return count < item.maxCount;
    }

    public void Increase()
    {
        count++;
        onChange?.Invoke();
    }

    public void Decrease()
    {
        count--;
        if (count == 0)
        {
            Clear();
        }
        else
        {
            onChange?.Invoke();
        }
    }

    public void AddItem(ItemData item)
    {
        this.item = item;
        count = 1;
        onChange?.Invoke();
    }

    public void AddListener(Action onChange)
    {
        this.onChange = onChange;
    }

    public void Clear()
    {
        item = null;
        count = 0;
        onChange?.Invoke();
    }
}
