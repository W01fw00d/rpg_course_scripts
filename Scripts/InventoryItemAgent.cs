using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemAgent : MonoBehaviour {

    public InventoryItem item;

    public Vector3 playerPosition;
    public Vector3 playerRotation;

    public bool hasBeenCollected = false;

    public void OnTriggerEnter(Collider other)
    {
        if (hasBeenCollected)
        {
            return;
        }

        if (other.gameObject.tag.Equals("Player"))
        {
            hasBeenCollected = true;

            InventoryItem collectedItem = new InventoryItem();
            collectedItem.CopyInventoryItem(item);
            GameMaster.sharedInstance.inventory.AddItem(collectedItem);
            GameMaster.sharedInstance.GameObjectDestroy(gameObject);
        }
    }
}
