using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemMoveHandler : MonoSingleton<ItemMoveHandler>
{
    private Image movingIcon;
    private SlotData selectedItem;
    private Player player;

    protected override void Awake()
    {
        base.Awake();

        movingIcon = GetComponentInChildren<Image>();
        player = FindAnyObjectByType<Player>();

        HideIcon();
    }

    private void Update()
    {
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
                DropItem();
                selectedItem.Clear();
                Clear();
                HideIcon();
            }
        }
    }

    private void DropItem()
    {
        if (selectedItem == null)
        {
            return;
        }

        for (int i = 0; i < selectedItem.count; i++)
        {
            Vector2 direction = Random.insideUnitCircle.normalized * 1.2f;
            Vector3 position = player.transform.position + (Vector3)direction;
            Instantiate(selectedItem.item.prefab, position, Quaternion.identity);
        }
    }

    public void HideIcon()
    {
        movingIcon.enabled = false;
    }

    public void ShowIcon(SlotData data)
    {
        selectedItem = data;
        movingIcon.sprite = data.item.sprite;
        movingIcon.enabled = true;
    }

    public void Clear()
    {
        selectedItem = null;
    }
}
