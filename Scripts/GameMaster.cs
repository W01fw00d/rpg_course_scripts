using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    public static GameMaster sharedInstance;

    public bool showOptions = false;

    public float musicVolume = 0;
    public float sfxVolume = 0;

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
        SceneManager.LoadScene("Chapter1");
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
