using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ListExample : MonoBehaviour
{
    public InputField input;
    public TextAsset textFileWithNames;
    public Text display;

    private List<string> namesList;

    [SerializeField] GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        // Instantiate the list
        namesList = new List<string>();
        
        // The below code reads the text file and splits it into lines.
        var namesFromFile = textFileWithNames.text.Split('\n');

        //List temp = new List(namesFromFile);

        // This code loops though every single line in the text file
        for (var i = 0; i < namesFromFile.Length; i++)
        {
            // Add each line to the list of namesList.
            namesList.Add(namesFromFile[i].ToUpper().Trim());
        }
        
        
        // Extra methods tests:
        Debug.Log("Count: " + namesList.Count);
        //Debug.Log("Names contains \"RED\":" + namesList.Contains("RED"));
       // Debug.Log("Names contains \"BLUE\":" + namesList.Contains("BLUE"));

        //Debug.Log("Contains both: " + 
        //          (namesList.Contains("RED") && 
        //           namesList.Contains("BLUE")));
        
        //namesList.Insert(5, "YELLOW");

       
    }

    public void DidValueChange()
    {
        // If there's nothing in the text box, show instructions.
        if (input.text == "")
        {
            //display.text = "Type a name to see if it's in the list!";
        }
        // Otherwise, check to see if the name is in the list.
        else
        {
            // Start by setting the display to say "not in list".
            display.text = "Who?";


            // Loop through the entire list
            for (int i = 0; i < namesList.Count; i++)
            {
                // If any of the namesList in the list match what in the input field,
                // say it's in the list.
                if (input.text.ToUpper() == namesList[i])
                {
                    display.text = "Okay, they were there...";
                }

            }



           
        }

    }

    private void Update()
    {
        if (gameManager.isGameOver == false)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (input.text.ToUpper() == "BLUE")
                {
                    display.text = "You got 'em!";
                    Debug.Log("You got 'em!");

                    gameManager.EndGame();
                }

                else
                {
                    display.text = "Nope! Try again!";
                    Debug.Log("Try again!");
                    gameManager.Score -= 50;
                }

            }

        }

        else
        {
            input.interactable = false;
        }
    }
}
