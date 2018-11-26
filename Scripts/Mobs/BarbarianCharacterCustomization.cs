using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarbarianCharacterCustomization : MonoBehaviour {

    public static BarbarianCharacterCustomization sharedInstance;

    CharacterSelectionManager manager;
    Canvas configCanvas;

    static string modelPath = "Geometry/model:geo/model:";

    private static string[] modelNames = new string[] {
        "Hair",
        "Strap",
        "Skull"
        // Brecer, Belt, Neckless, SandelL, SandalR, Bandages, AnkletL, AnkletR
    };
    public Hashtable mapping = createModelMapping(modelNames);

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }

        manager = GameObject.Find("CharacterSelectionManager").GetComponent<CharacterSelectionManager>();
        configCanvas = GameObject.Find("CharacterCustomizationCanvas").GetComponent<Canvas>();
    }

    public void ToggleCanvas()
    {
        if (manager.selectedCharacter.GetComponent<BarbarianCharacterController>())
        {
            configCanvas.enabled = true;
        } else
        {
            configCanvas.enabled = false;
        }
    }

	void Start () {

    }
	
	void Update () {
		
	}

    public void SetVisiblePad(Toggle toggle)
    {

        foreach (DictionaryEntry map in mapping)
        {
            if (map.Key.Equals(toggle.name))

                // ToDo Doesnt unity have a ToggleActive()? If not, I should implement it for GameObject
                manager.selectedCharacter.gameObject.transform.Find(map.Value as string)
                .gameObject.SetActive(
                    !manager.selectedCharacter.gameObject.transform.Find(map.Value as string)
                    .gameObject.activeInHierarchy
                );
        }
    }

    // ToDo and another to update them and check them


    public Hashtable sliderMapping = new Hashtable()
    {
        { "RedSlider", 1.0f },
        { "GreenSlider", 1.0f },
        { "BlueSlider", 1.0f },
        { "XSlider", 1.0f },
        { "YSlider", 1.0f },
        { "ZSlider", 1.0f },
    };

    public void ChangeSlideValue(Slider slider)
    {
        sliderMapping[slider.name] = slider.value;

        Debug.Log((float)sliderMapping["XSlider"]);

        manager.selectedCharacter.transform.localScale = new Vector3(
            (float) sliderMapping["XSlider"],
            (float) sliderMapping["YSlider"],
            (float) sliderMapping["ZSlider"]
        );

        foreach(Renderer renderer in manager.selectedCharacter.GetComponentsInChildren<Renderer>())
        {
            renderer.material.color = new Color(
                (float) sliderMapping["RedSlider"],
                (float) sliderMapping["GreenSlider"],
                (float) sliderMapping["BlueSlider"]
            );
        }
    }

    public void ChangeDropdownValue(Dropdown dropdown)
    {

    }

    static private Hashtable createModelMapping(string[] names)
    {
        Hashtable result = new Hashtable();

        foreach (string name in names)
        {
            result.Add("Toggle" + name, modelPath + name);
        }

        return result;
    }

    static private Hashtable createSlidersMapping(string[] names)
    {
        Hashtable result = new Hashtable();

        foreach (string name in names)
        {
            result.Add("Toggle" + name, modelPath + name);
        }

        return result;
    }
}
