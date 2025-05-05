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

    int score = 0;

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

    private const string fileName = "highScores.txt";
    string filePath = "";




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

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void EndGame()
    {
        isGameOver = true;
        SceneManager.LoadScene("Game Over Scene");
    }

    public void WinGame()
    {
        isGameOver = true;
        SceneManager.LoadScene("Game Win Scene");
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
