using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseCharacter
{
    [SerializeField]
    public string name;

    [SerializeField]
    public string description;

    [SerializeField]
    public float 
        strength, //Daño físico que causa nuestro personaje
        defense, //Daño físico que puede recibir
        dexterity, //Mide la "habilidad" del personaje
        intelligence, //mide la capacidad de razonamiento / interacción
        health; // marca si el positivo está vivo o muerto

    public bool canUseWeapons;
    public Transform weaponSpot;
    public GameObject currentWeapon;
}
