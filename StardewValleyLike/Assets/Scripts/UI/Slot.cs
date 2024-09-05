using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public int index;
    public Image iconImage;
    public TextMeshProUGUI countText;

    private SlotData data;

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemMoveHandler.Instance.ShowIcon(data);
    }

    public void SetData(SlotData data)
    {
        this.data = data;
        data.AddListener(OnDateChagne);

        UpdateUI();
    }

    private void OnDateChagne()
    {
        UpdateUI();
    }

    private void UpdateUI()
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
