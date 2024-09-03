using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private Dictionary<ItemType, ItemData> itemDatas = new Dictionary<ItemType, ItemData>();

    public InventoryData backpack;

    private void Awake()
    {
        Instance = this;
        Init();
    }

    private void Init()
    {
        foreach (var itemData in Resources.LoadAll<ItemData>("Data"))
        {
            itemDatas.Add(itemData.type, itemData);
        }

        backpack = Resources.Load<InventoryData>("Data/Backpack");
    }

    public ItemData GetItemData(ItemType type)
    {
        ItemData data;
        if (itemDatas.TryGetValue(type, out data))
        {
            return data;
        }

        Debug.LogWarning("GetItemData: not ItemData for ItemType " + type);
        return null;
    }

    public void AddToBackpack(ItemType type)
    {
        var itemData = GetItemData(type);
        if (itemData == null)
        {
            return;
        }

        foreach (var slot in backpack.slots)
        {
            if (slot.item == itemData && slot.CanAddItem())
            {
                slot.Increase();
                return;
            }
        }

        foreach (var slot in backpack.slots)
        {
            if (slot.count == 0)
            {
                slot.AddItem(itemData);
                return;
            }
        }

        Debug.LogWarning("AddToBackpack fail: " + type);
    }
}
