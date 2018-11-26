using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class PlayerAgent : MonoBehaviour {

    public PlayerCharacter playerCharacterData;

	// Use this for initialization
	void Start () {
        PlayerCharacter tmp = new PlayerCharacter();
        tmp.name = "Er Prota";
        tmp.health = 100;
        tmp.defense = 50;
        tmp.description = "Como mola";
        tmp.dexterity = 30;
        tmp.intelligence = 10;
        tmp.strength = 40;

        playerCharacterData = tmp;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerCharacterData.health < 0.0f)
        {
            playerCharacterData.health = 0;

            transform.GetComponent<BarbarianCharacterController>().die = true;
        }
	}
}
