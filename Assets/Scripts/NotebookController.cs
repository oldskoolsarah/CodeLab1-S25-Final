using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotebookController : MonoBehaviour
{
    int clueCount = 0;

    private List<string> myClueList;

    [SerializeField] private TextMeshProUGUI NotebookClues = null;

    [SerializeField] GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NotebookClues.text = "";
        Debug.Log("Notebook enable");


    }

    // Update is called once per frame
    void Update()
    {
      if (gameManager.isGameOver == false)
        {
            
      
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Notebook updated");
         
            OnEnable();

            ++clueCount;

            gameManager.Score -= 10;

            Debug.Log(clueCount);
        }

       }

    }

    public void OnEnable()
    {
        
        if (NotebookClues == null)
        {
            return;
        }

        if (clueCount == gameManager.enabledClueList.Count)
        {
            NotebookClues.text += "No more clues." + "\n";
            return;
        }

        if (clueCount >= gameManager.enabledClueList.Count)
        {
            
            return;
        }

        
        NotebookClues.text += gameManager.enabledClueList[clueCount] + "\n";

        //for (int i = 0; i < myClueList.Count; i++)
        //{
        //    //NotebookClues.text += myClueList[i].Text + "\n";
        //    NotebookClues.text += myClueList[i] + "\n";
        //}
    }

}
