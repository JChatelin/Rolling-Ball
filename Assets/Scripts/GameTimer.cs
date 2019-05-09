using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameTimer : MonoBehaviour
{
    private Text timerText;

    private float gameTimer;

    private int minutes;
    private int seconds;

    private bool start;

    private void Start()
    {
        gameTimer = 0.0f;
        start = false;
    }
    void Update()
    {
        if(start)
        {
            RunTimer();
        }
    }

    // Convert the since the begining of a level into 'minutes: seconds' format
    private void RunTimer()
    {
        gameTimer += Time.deltaTime;
        minutes = (int)((gameTimer / 60) % 60);
        seconds = (int)(gameTimer % 60);
        DisplayTimer();
    }

    // Use to stop the timer from the game controller
    public void StopTimer()
    {
        start = false;
    }

    // Use to start the timer from the game controller
    public void StartTimer()
    {
        start = true;
    }

    public void ResetTimer()
    {
        gameTimer = 0.0f;
        minutes = 0;
        seconds = 0;
    }
    public void SetTimerText(Text timerText)
    {
        this.timerText = timerText;
        // Initialize the timer with 00:00 at the begin on the game
        this.timerText.text = timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void DisplayTimer()
    {
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timerString;
    }
    public int Seconds
    {
        get { return seconds; }
    }

    public int Minutes
    {
        get { return minutes; }
    }
}
