using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour {

    public GameObject inventoryPanel;
    public Transform inventoryPanelItem;
    public GameObject inventoryItemElement;

    // Use this for initialization
    void Start () {
       inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        if (
            GameMaster.sharedInstance.currentScene.name != SceneName.mainMenu &&
            GameMaster.sharedInstance.currentScene.name != SceneName.characterSelection
        ) {
            if (Input.GetKeyUp(KeyCode.I))
            {
                inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
            }
        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            GameObject newButton = Instantiate(inventoryItemElement) as GameObject;
            InventoryItemUI inventoryItemUI = newButton.GetComponent<InventoryItemUI>();
            inventoryItemUI.itemElementText.text = string.Format("Nuevo ítem {0}", Time.time);
            newButton.transform.SetParent(inventoryPanelItem);

            //inventoryItemUI.equipButton.GetComponent<Button>().onClick.AddListener(
            //    () =>
            //    {

            //    }
            //);

            //newButton.transform.SetParent(inventoryPanelItem);

        }


    }
}
