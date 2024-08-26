using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour
{
    /*
    public int playerID;

    [SerializeField] ADGameManager gameManager;
    [SerializeField] CharacterSelect characterSelect;
    [SerializeField] string playerName;


    [Header ("UI Elements")]
    [SerializeField] Image characterImage;
    [SerializeField] TextMeshProUGUI characterText;
    [SerializeField] TMP_InputField playerInputField;

    int currentCharacter = 0;

    private void Start()
    {
        SetCharacter(currentCharacter);
    }
    

    public void NextCharacter()
    {
        currentCharacter++;
        if (currentCharacter >= characterSelect.characters.Count)
        {
            currentCharacter = 0;
        }

        SetCharacter(currentCharacter);
    }

    public void PreviousCharacter()
    {
        currentCharacter--;
        if (currentCharacter < 0)
        {
            currentCharacter = characterSelect.characters.Count - 1;
        }

        SetCharacter(currentCharacter);
    }

    public void SetCharacter(int id)
    {
        characterImage.sprite = characterSelect.characters[id].characterSprite;
        characterText.text = characterSelect.characters[id].characterName;
    }

    public void Confirm()
    {
        playerName = playerInputField.text;
        gameManager.SetPlayer(playerID, currentCharacter, playerName);
    }
    */
}
