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

    private GameObject player;
    private GameObject startPosition;

    public Scene currentScene;

    private void OnLevelWasLoaded(int level)
    {
        currentScene = SceneManager.GetActiveScene();
        startPosition = GameObject.Find("GameStartPosition");
        player = GameObject.FindWithTag("Player");
        player.transform.position = Vector3.zero;

        Debug.Log(currentScene);
        Debug.Log(startPosition);
        Debug.Log(player);

        if (currentScene.name.Equals(SceneName.level1))
        {
            Instantiate(player, startPosition.transform);
        }
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
