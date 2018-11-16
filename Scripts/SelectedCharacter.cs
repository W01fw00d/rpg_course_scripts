using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCharacter : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject tempLight = GameObject.Find("spotlight");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        GameMaster.sharedInstance.player = 
            GameObject.Find("CharacterSelectionManager").GetComponent<CharacterSelectionManager>().selectedCharacter;

    }
}
