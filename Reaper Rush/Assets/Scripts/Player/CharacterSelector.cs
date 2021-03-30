using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public int currentCharacterIndex;
    public GameObject[] characters;
    public string runnerName;
    public string runner;


    void Start()
    {
        currentCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach (GameObject character in characters)
            character.SetActive(false);

        characters[currentCharacterIndex].SetActive(true);
        runnerName = characters[currentCharacterIndex].name;

    }
    void Update()
    {
        
      if (GameObject.Find("GameManager"))
            {

            runner = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().currentRunner;
        }

        foreach (GameObject character in characters)
        {
            if (character.name == runner)
            {
                Debug.Log(character.name);
                character.SetActive(true);
            }
            else if (character.name != runner)
            {
                character.SetActive(false);
            }

        }
}
}
