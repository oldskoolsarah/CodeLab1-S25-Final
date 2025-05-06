using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // Game controller singleton
    public static GameManager instance;

    public bool isGameOver = false;

    public Timer timer;

    public int score = 0;

    public int currentHighScore = 0;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            //UpdateHighScores(score);

        }

    }

    [SerializeField] List<int> highScores;

    public List<string> myClues;

    public List<string> enabledClueList = new List<string>();


    private const string fileName = "highScores.txt";
    string filePath = "";

    private const string fileName2 = "listOfClues.txt";
    string filePath2 = "";


    void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            // This will make the object this script is attached to persist between scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        filePath = Application.dataPath + "/Data/" + fileName;

        filePath2 = Application.dataPath + "/Data/" + fileName2;

        timer = GetComponent<Timer>();

        //check if high score file exists
        if (File.Exists(filePath))
        {
            //if it does, read the file
            string fileContents = File.ReadAllText(filePath);

            //string[] lines = fileContents.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string[] lines = fileContents.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                highScores.Add(int.Parse(line));
            }

            for (int i = 0; i < highScores.Count; i++)
            {

                if (highScores[i] > currentHighScore)
                {
                    currentHighScore = highScores[i];
                }
                
            }

           
        }
        else //do nothing
        {
            ////otherwise set high score to dummy values of 1 - 10
            //for (int i = 0; i < 10; i++)
            //{
            //    highScores.Add(10 - i);
            //}
        }

        if (File.Exists(filePath2))
        {
            //if it does, read the file
            string fileContents = File.ReadAllText(filePath2);

            string[] lines = fileContents.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                myClues.Add((line));
            }

            Debug.Log("Clue list loaded: " + myClues.Count);
            Debug.Log("Clue list: " + string.Join(", ", myClues));
        }
        else //do nothing
        {
          

        }

    }

    public void UpdateClueList(List<string> myClues)
    {
        System.Random randomGenerator = new System.Random();
        string randomClueFromMyClues;

        while (myClues.Count > 0)
        {
            // Pick a random clue from myClues that is NOT in the enabledClueList (yet)
            randomClueFromMyClues = myClues[UnityEngine.Random.Range(0,myClues.Count)];

            //Add the randomly picked clue to enabledClueList
            enabledClueList.Add(randomClueFromMyClues);

            // remove it from myClues
            myClues.Remove(randomClueFromMyClues);

            // Check if there are no more clues left in myClues
            if (myClues.Count == 0)
            {
                //return "No additional clues";
                Debug.Log("No additional clues");
                return;
            }

        }


    }


// Update is called once per frame
void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key was pressed");

            UpdateClueList(myClues);
        }
    }

    public void EndGame()
    {
        isGameOver = true;

        score += (timer.gameTime - timer.currentTime);

        timer.StopTimer();

        UpdateHighScores();

        timer.timerDisplay.text = timer.TimerString(timer.gameTime - timer.currentTime) + "   Game Over" ;

        //SceneManager.LoadScene("Game Over Scene");
    }


    public void UpdateHighScores()
    {
        for (int i = 0; i < highScores.Count; i++)
        {
            int currentHS = highScores[i];

            if (score >= currentHS)
            {
                highScores.Insert(i, score);
                //highScores.RemoveAt(highScores.Count - 1);

                currentHighScore = score;

                break;
            }
            if (highScores.Count > 10)
            {
                highScores.RemoveAt(highScores.Count - 1);
            }

        }

        

        string fileContents = "";

        foreach (var scoreData in highScores)
        {
            //fileContents += scoreData + "\n";
            fileContents += scoreData + ",";
        }

        File.WriteAllText(filePath, fileContents);

    }
}
