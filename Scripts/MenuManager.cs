using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public RectTransform optionsPanel;

    public Slider mainVolumeSlider;
    public Slider sfxVolumeSlider;

    void Start () {
        mainVolumeSlider.value = GameMaster.sharedInstance.musicVolume;
        sfxVolumeSlider.value = GameMaster.sharedInstance.sfxVolume;
    }

    void Update () {
		
	}

    public void StartGame()
    {
        GameMaster.sharedInstance.StartGame();
    }

    public void ShowOptions()
    {
        ToggleOptions();
    }

    public void CloseOptions()
    {
        ToggleOptions();
    }

    public void ExitGame()
    {

    }

    public void MainVolume()
    {
        GameMaster.sharedInstance.MainVolume(mainVolumeSlider.value);
    }

    public void SFXVolume()
    {
        GameMaster.sharedInstance.SFXVolume(sfxVolumeSlider.value);
    }

    private void ToggleOptions()
    {
        GameMaster.sharedInstance.showOptions = !GameMaster.sharedInstance.showOptions;
        optionsPanel.gameObject.SetActive(GameMaster.sharedInstance.showOptions);
    }
}
