using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int index;
    public Image iconImage;
    public TextMeshProUGUI countText;

    public void SetData(SlotData data)
    {
        if (data.item == null || data.count == 0)
        {
            iconImage.enabled = false;
            countText.enabled = false;
        }
        else
        {
            iconImage.enabled = true;
            iconImage.sprite = data.item.sprite;

            countText.enabled = true;
            countText.text = "" + data.count;
        }
    }
}
