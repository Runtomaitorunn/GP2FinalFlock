using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OutsideWordManager : MonoBehaviour
{
    private ReadTextInput readTextInput;
    [SerializeField]private string outsideText;
    private TextMeshProUGUI textBox;
    public GameObject rockPrefab;

    
    
    public void GenerateRock(string outsideText)
    {
        // text with the rock
        PrintOutText(outsideText);

        // generate rock
        // randomly place the rock
        float x = Random.Range(-10.68f, 12.55f);
        float z = Random.Range(-2.62f,24.77f);
        GameObject rock = Instantiate(rockPrefab, transform);
        rock.transform.position = new Vector3(x,-10.26f,z);

        // ramdomly select rock sprite
    }
    
    public void PrintOutText(string outsideText)
    {
        // Text on the rock
        textBox = rockPrefab.GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log("the rock received" + outsideText);
        textBox.text = outsideText;
    }
}
