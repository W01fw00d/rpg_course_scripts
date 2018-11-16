using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneName
{
    public const string mainMenu = "Main Menu";
    public const string characterSelection = "Character Selection";
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

    private void OnLevelWasLoaded(int level)
    {
        currentScene = SceneManager.GetActiveScene();

        startPosition = GameObject.Find("GameStartPosition");

        player = (GameObject)Resources.Load("Prefabs/" + characterName);

        //foreach (GameObject possiblePlayer in GameObject.FindGameObjectsWithTag("Player"))
        //{
        //    if (possiblePlayer.name.Equals(characterName))
        //    {
        //        player = possiblePlayer;
        //    } else
        //    {
        //        possiblePlayer.SetActive(false);
        //    }
        //}

        if (player == null || !currentScene.name.Equals(SceneName.level1))
        {
            return;
        }
           
        player.GetComponentInChildren<Camera>().enabled = true;
        //character.GetComponent<Rigidbody>().useGravity = true;

        if (player.GetComponent<BarbarianCharacterController>() != null)
        {
            player.GetComponent<BarbarianCharacterController>().enabled = true;
        }

        if (player.GetComponent<DragonCharacterController>() != null)
        {
            player.GetComponent<DragonCharacterController>().enabled = true;
        }

        //player.GetComponent<SelectedCharacter>().enabled = currentScene.name.Equals(SceneName.characterSelection);
        player.transform.position = Vector3.zero;

        //Debug.Log(currentScene);
        //Debug.Log(startPosition);
        //Debug.Log(player);

        Instantiate(player, startPosition.transform);
    }

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        } else if (sharedInstance != this)
        {
            Destroy(this);
        }

        MainVolume(sharedInstance.GetComponent<AudioSource>().volume);

        DontDestroyOnLoad(this);
    }

    public void StartGame()
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
}
