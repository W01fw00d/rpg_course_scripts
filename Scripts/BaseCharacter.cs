using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
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



}
