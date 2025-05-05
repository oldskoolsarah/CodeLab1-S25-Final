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

    //public bool isGameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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

            timerDisplay.text = "Game Over!" + " Your Score: " + GameManager.instance.Score + " High Score: " + GameManager.instance.currentHighScore;
            
            //isGameOver = true;
           
            CancelInvoke(TimerTick);
        }
        else
        {
            timerDisplay.text = TimerString(gameTime - currentTime);
            //Invoke(TimerTick, 1);

        }
    }

    string TimerString(int timeInt)
    {
        string result = "";

        result = "Time: " + timeInt + " Score: " + GameManager.instance.Score + " High Score: " + GameManager.instance.currentHighScore;

        return result;
    }

}
