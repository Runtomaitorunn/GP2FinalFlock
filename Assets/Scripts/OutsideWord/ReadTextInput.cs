using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadTextInput : MonoBehaviour
{
    private string inputText = "";
    public TMP_InputField inputField;
    private OutsideWordManager outsideWordManager;
    public GameManager gameManager;

    private void Awake()
    {
       outsideWordManager = GetComponent<OutsideWordManager>();
       GameManager gameManager = GetComponent<GameManager>();
        if (gameManager == null)
        {
            Debug.Log("cannot fine game manager");
        }

    }

    public void TakeStringInput(string text)
    {
        // Read input
        inputText = text;
        Debug.Log(inputText);

        // pass the input
        outsideWordManager.GenerateRock(inputText);
        gameManager.StartIdeas(inputText);


        //Clean out the textbox
        CleanStringInput();

    }

    public void CleanStringInput()
    {
        inputField.text = "";
    }
}
