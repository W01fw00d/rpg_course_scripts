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

    // ToDo IMplement here a function that maps name of slider and its value as floats
    float red = 1,
        green = 1,
        blue = 1,
        xs = 1,
        ys = 1,
        zs = 1;
    // ToDo and another to update them and check them

    public void ChangeSlideValue(Slider slider)
    {

        // ToDo Call here the method to update

        manager.selectedCharacter.transform.localScale = new Vector3(xs, ys, zs);

        foreach(Renderer renderer in manager.selectedCharacter.GetComponentsInChildren<Renderer>())
        {
            renderer.material.color = new Color(red, green, blue);
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
}
