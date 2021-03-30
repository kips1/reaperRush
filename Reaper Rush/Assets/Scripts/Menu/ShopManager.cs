using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopManager : MonoBehaviour
{
    public int currentCharacterIndex;
    public GameObject[] characterModels;

    public GameObject options;

    public CharacterBlueprint[] characters;
    public Button buyButton;
    void Start()
    {
        foreach (CharacterBlueprint character in characters)
        {
            if (character.price == 0)
                character.isUnlocked = true;
            else
                character.isUnlocked = PlayerPrefs.GetInt(character.name, 0) == 0 ? false : true;
        }

        options = GameObject.FindGameObjectWithTag("Options");
        currentCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach (GameObject character in characterModels)
            character.SetActive(false);

        characterModels[currentCharacterIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        options.GetComponent<Options>().runnerName = characterModels[currentCharacterIndex].name;
        UpdateUI();
    }

    public void ChangeNext()
    {
        characterModels[currentCharacterIndex].SetActive(false);
        currentCharacterIndex++;

        if (currentCharacterIndex == characterModels.Length)
            currentCharacterIndex = 0;

        characterModels[currentCharacterIndex].SetActive(true);

        CharacterBlueprint c = characters[currentCharacterIndex];
        if (!c.isUnlocked)
            return;

        PlayerPrefs.SetInt("SelectedCharacter", currentCharacterIndex);
    }

    public void ChangePrevious()
    {
        characterModels[currentCharacterIndex].SetActive(false);
        currentCharacterIndex--;

        if (currentCharacterIndex == -1)
            currentCharacterIndex = characterModels.Length - 1;

        characterModels[currentCharacterIndex].SetActive(true);

        CharacterBlueprint c = characters[currentCharacterIndex];
        if (!c.isUnlocked)
            return;
        PlayerPrefs.SetInt("SelectedCharacter", currentCharacterIndex);
    }

    public void UnlockCharacter()
    {
        CharacterBlueprint c = characters[currentCharacterIndex];
         
        PlayerPrefs.SetInt(c.name, 1);
        PlayerPrefs.SetInt("SelectedCharacter", currentCharacterIndex);
        c.isUnlocked = true;
        PlayerPrefs.SetInt("coinAmount", PlayerPrefs.GetInt("coinAmount", 0) -c.price);
    }

    private void UpdateUI()
    {
        CharacterBlueprint c = characters[currentCharacterIndex];
        if (c.isUnlocked)
        {
            buyButton.gameObject.SetActive(false);

        }
        else
        {
            buyButton.gameObject.SetActive(true);
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Buy-" + c.price;
            if (c.price >= PlayerPrefs.GetInt("coinAmount", 0))
            {
                buyButton.interactable = true;
            }
            else
            {
                buyButton.interactable = false;

            }

        }
    }
}
