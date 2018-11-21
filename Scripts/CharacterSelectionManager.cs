using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionManager : MonoBehaviour
{
    public GameObject[] availableCharacters;
    private Transform spawnPoint;

    int characterId = 0;
    public GameObject selectedCharacter;

    private void Awake()
    {
        spawnPoint = GameObject.Find("SpawnPoint").transform;
        //foreach (GameObject character in availableCharacters)
        //{
        //    character.GetComponentInChildren<Camera>().enabled = false;
        //    //character.GetComponent<Rigidbody>().useGravity = false;

        //    if (character.GetComponent<BarbarianCharacterController>() != null)
        //    {
        //        character.GetComponent<BarbarianCharacterController>().enabled = false;
        //    }

        //    if (character.GetComponent<DragonCharacterController>() != null)
        //    {
        //        character.GetComponent<DragonCharacterController>().enabled = false;
        //    }
        //}
    }

    private void SpawnCharacter()
    {
        if (selectedCharacter != null)
        {
            Destroy(selectedCharacter);
        }

        selectedCharacter = Instantiate(availableCharacters[characterId], spawnPoint);

        selectedCharacter.AddComponent<SelectedCharacter>();

        BarbarianCharacterCustomization.sharedInstance.ToggleCanvas();

        //selectedCharacter.transform.position = spawnPoint.transform.position;
    }

    void Start()
    {
        SpawnCharacter();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            characterId = Mathf.Clamp(characterId - 1, 0, availableCharacters.Length - 1);
            SpawnCharacter();
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            characterId = Mathf.Clamp(characterId + 1, 0, availableCharacters.Length - 1);
            SpawnCharacter();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            spawnPoint.transform.Rotate(new Vector3(0, 1, 0), 90.0f * Time.deltaTime);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            spawnPoint.transform.Rotate(new Vector3(0, -1, 0), 90.0f * Time.deltaTime);
        }
    }

    private void OnMouseDown()
    {
        GameMaster.sharedInstance.characterName = GameObject.Find("CharacterSelectionManager").
             GetComponent<CharacterSelectionManager>().
             selectedCharacter.name.Replace("(Clone)", "");
    }
}
