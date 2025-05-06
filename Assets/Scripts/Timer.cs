using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerDisplay;

    public int gameTime = 10;

    public int currentTime = 0;

    public const string TimerTick = "UpdateTimer";

    [SerializeField] GameManager gameManager;

    //public bool isGameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        gameManager = FindObjectOfType<GameManager>();

        timerDisplay.text = TimerString(gameTime);
        //timerDisplay.text = gameTime.ToString();

        //Invoke(TimerTick, 1);
        InvokeRepeating(TimerTick, 1, 1);

    }

    public void UpdateTimer()
    {
        currentTime++;
        
        if (currentTime == gameTime)
        {
            GameManager.instance.UpdateHighScores();

            timerDisplay.text = "Game Over!"; 

            //timerDisplay.text = "Game Over!" + " Your Score: " + GameManager.instance.Score + " High Score: " + GameManager.instance.currentHighScore;

            //isGameOver = true;

            CancelInvoke(TimerTick);

            gameManager.EndGame();
        }
        else
        {
            timerDisplay.text = TimerString(gameTime - currentTime);
            //Invoke(TimerTick, 1);

        }
    }

    public void StopTimer()
    {
        CancelInvoke(TimerTick);
        
    }


    public string TimerString(int timeInt)
    {
        string result = "";

        result = "Time: " + timeInt + " Score: " + gameManager.Score + " High Score: " + gameManager.currentHighScore;

        return result;
    }

}
