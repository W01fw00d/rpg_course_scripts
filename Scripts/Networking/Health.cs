using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Health : NetworkBehaviour
{
    public const int MAX_HEALTH = 100;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = MAX_HEALTH;

    public Slider healthBar;
    public bool destroyOnDeath;
    public GameObject[] listOfPlayers;

	void Start ()
    {
        healthBar.maxValue = MAX_HEALTH;
        healthBar.value = MAX_HEALTH;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (destroyOnDeath && currentHealth <= 0)
        {
            RpcDied();
            //listOfPlayers = GameObject.FindGameObjectsWithTag("Enemy"); // Cooperativo
            listOfPlayers = GameObject.FindGameObjectsWithTag("Player"); // Todos contra todos

            if (listOfPlayers.Length <= 0)
            {
                Invoke("BackToLobby", 3.0f);
            } else
            {
                currentHealth = MAX_HEALTH;
                RpcRespawn();
            }
        }
    }

    void OnChangeHealth(int newHealth)
    {
        healthBar.value = newHealth;
    }

    [ClientRpc]
    void RpcDied()
    {
        gameObject.tag = "Untagged";

        foreach (Transform child in transform)
        {
            if (child.GetComponent<Renderer>() != null)
            {
                GetComponent<Renderer>().material.color = Color.black;
            }
        }

        if (GetComponent<MyPlayerController>() != null)
        {
            GetComponent<MyPlayerController>().enabled = false;
        }

        if (GetComponent<EnemyController>() != null)
        {
            GetComponent<EnemyController>().enabled = false;
        }
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            transform.position = Vector3.zero;
        }
    }

    void BackToLobby()
    {
        FindObjectOfType<NetworkLobbyManager>().ServerReturnToLobby();
    }
}
