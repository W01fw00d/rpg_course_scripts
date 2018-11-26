using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCharacter : MonoBehaviour {

    public Light playerLight;

    void Start()
    {
        GameObject tempLight = GameObject.Find("SpotLight");
        playerLight.enabled = false;
    }

    void OnMouseEnter()
    {
        playerLight.enabled = true;
    }

    void OnMouseOver()
    {
    }

    void OnMouseExit()
    {
        playerLight.enabled = false;
    }

    private void OnMouseDown()
    {
        GameMaster.sharedInstance.player = 
            GameObject.Find("CharacterSelectionManager").
                GetComponent<CharacterSelectionManager>().selectedCharacter;

        GameMaster.sharedInstance.LoadFirstLevel();
    }
}
