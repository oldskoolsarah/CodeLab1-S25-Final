using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    private GameObject instructionCanvas;
    private GameObject instructionPanel1;
    private GameObject instructionPanel2;
    //private GameObject instructionPanel3;
    //private GameObject instructionPanel4;

    private AudioSource audioSource;
    private int spaceCount = 0;

    private void Awake()
    {
        instructionCanvas = GameObject.Find("InstructionCanvas");
        if (instructionCanvas == null)
        {
            Debug.Log("InstructionCanvas not found");
        }

        instructionPanel1 = GameObject.Find("Instruction1");
        if (instructionPanel1 == null)
        {
            Debug.Log("Instruction panel 1 not found");
        }
        else
        {
            instructionPanel1.SetActive(false);
        }

        instructionPanel2 = GameObject.Find("Instruction2");
        instructionPanel2.SetActive(false);

        //instructionPanel3 = GameObject.Find("Instruction3");
        //instructionPanel3.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    public void PressPlay()
    {
        instructionPanel1.SetActive(true);
        audioSource.Play();
    }

    private void Update()
    {
        if (instructionPanel1.activeSelf && spaceCount == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                spaceCount = 1;
                instructionPanel1.SetActive(false);
                instructionPanel2.SetActive(true);
                audioSource.Play();
            }
        }
        else if (instructionPanel2.activeSelf && spaceCount == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("GamePlayScene");
            }
        }
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
}
