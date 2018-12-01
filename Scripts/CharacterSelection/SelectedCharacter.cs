using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectedCharacter : MonoBehaviour {

    //public Light playerLight;

    public Scene currentScene;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    void Start()
    {
        //GameObject tempLight = GameObject.Find("SpotLight");
        //playerLight.enabled = false;
    }

    void OnMouseEnter()
    {
        //playerLight.enabled = true;
    }

    void OnMouseOver()
    {
    }

    void OnMouseExit()
    {
        //playerLight.enabled = false;
    }

    private void OnLevelWasLoaded(int level)
    {
        currentScene = SceneManager.GetActiveScene();
    }

    private void OnMouseDown()
    {
        if (currentScene.name.Equals("CharacterSelection"))
        {
            GameObject selectedCharacter = GameObject.Find("CharacterSelectionManager").
            GetComponent<CharacterSelectionManager>().selectedCharacter;

            selectedCharacter.transform.parent = null;
            selectedCharacter.AddComponent<NonDestroyable>();
            GameMaster.sharedInstance.player = selectedCharacter;

            GameMaster.sharedInstance.LoadFirstLevel();
        }
    }
}
