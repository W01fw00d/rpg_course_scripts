using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneName
{
    public const string mainMenu = "MainMenu";
    public const string characterSelection = "CharacterSelection";
    public const string level1 = "Chapter1";
}

[RequireComponent(typeof(AudioSource))]
public class GameMaster : MonoBehaviour {

    public static GameMaster sharedInstance;

    public bool showOptions = false;

    public float musicVolume = 0;
    public float sfxVolume = 0;

    public GameObject player;
    private GameObject startPosition;

    public Scene currentScene;

    public string characterName;

    public InventorySystem inventory;

    private void OnLevelWasLoaded(int level)
    {
        currentScene = SceneManager.GetActiveScene();

        startPosition = GameObject.Find("GameStartPosition");

        player = (GameObject)Resources.Load("Prefabs/" + characterName);

        if (player == null || !currentScene.name.Equals(SceneName.level1))
        {
            return;
        }
           
        player.GetComponentInChildren<Camera>().enabled = true;

        if (player.GetComponent<BarbarianCharacterController>() != null)
        {
            player.GetComponent<BarbarianCharacterController>().enabled = true;
        }

        if (player.GetComponent<DragonCharacterController>() != null)
        {
            player.GetComponent<DragonCharacterController>().enabled = true;
        }

        player.transform.position = Vector3.zero;

        Instantiate(player, startPosition.transform);
    }

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
            inventory = new InventorySystem();

            //
            InventoryItem tempItem = new InventoryItem();
            tempItem.Category = BaseItem.ItemCategory.Clothing;
            inventory.AddItem(tempItem);
            //

        } else if (sharedInstance != this)
        {
            Destroy(this);
        }

        MainVolume(sharedInstance.GetComponent<AudioSource>().volume);

        DontDestroyOnLoad(this);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneName.characterSelection);
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(SceneName.level1);
    }

    public void MainVolume(float newVolume)
    {
        this.musicVolume = newVolume;
        GetComponent<AudioSource>().volume = this.musicVolume;
    }

    public void SFXVolume(float newVolume)
    {
        this.sfxVolume = newVolume;
    }

    public void GameObjectDestroy(GameObject aGameObject)
    {
        Destroy(aGameObject);
    }
}
