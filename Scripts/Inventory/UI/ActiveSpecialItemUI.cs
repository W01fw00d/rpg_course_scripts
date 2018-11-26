using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActiveSpecialItemUI : EventTrigger
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        InventoryItem item = gameObject.GetComponent<PanelWeaponsItemUI>().item;
        switch (item.Category)
        {
            case BaseItem.ItemCategory.Health:
                Destroy(gameObject);
                break;
        }
    }
}
