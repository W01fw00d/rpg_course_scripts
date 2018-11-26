using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAgent : MonoBehaviour {

    public BaseCharacter npcData;

    void Start()
    {
        PlayerCharacter tmp = new PlayerCharacter();
        tmp.name = "Er Pringao";
        tmp.health = 100;
        tmp.defense = 50;
        tmp.description = "No mola";
        tmp.dexterity = 30;
        tmp.intelligence = 10;
        tmp.strength = 40;

        npcData = tmp;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
