using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemMoveHandler : MonoSingleton<ItemMoveHandler>
{
    private Image movingIcon;
    private Slot selectedSlot;
    private Player player;
    private bool isCtrlPress;

    protected override void Awake()
    {
        base.Awake();

        movingIcon = GetComponentInChildren<Image>();
        player = FindAnyObjectByType<Player>();

        HideIcon();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) { isCtrlPress = true; }
        else if (Input.GetKeyUp(KeyCode.LeftControl)) { isCtrlPress = false; }

        if (movingIcon.enabled)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                GetComponent<RectTransform>(), Input.mousePosition, null, out Vector2 position);

            movingIcon.rectTransform.anchoredPosition = position;
        }

        if (Input.GetMouseButtonDown(0) && movingIcon.enabled)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (selectedSlot == null) { return; }
                var slotData = selectedSlot.GetData();
                DropItem(slotData.item.prefab, isCtrlPress ? 1 : slotData.count);
                if (isCtrlPress)
                {
                    slotData.Decrease();
                }
                if (slotData.count == 0)
                {
                    selectedSlot.Clear();
                    HideIcon();
                    Clear();
                }
            }
        }
    }

    private void DropItem(GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 direction = Random.insideUnitCircle.normalized * 1.2f;
            Vector3 position = player.transform.position + (Vector3)direction;
            Instantiate(prefab, position, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = direction * 0.15f;
        }
    }

    public void HideIcon()
    {
        movingIcon.enabled = false;
    }

    public void ShowIcon(Slot slot)
    {
        if (slot.GetData().item == null) { return; }
        selectedSlot = slot;
        movingIcon.sprite = slot.GetData().item.sprite;
        movingIcon.enabled = true;
    }

    public void Clear()
    {
        selectedSlot = null;
    }
}
