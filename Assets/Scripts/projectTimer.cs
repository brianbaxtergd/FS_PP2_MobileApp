using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class projectTimer : MonoBehaviour
{
    // State data.
    enum timerStates
    {
        STATE_WORK,
        STATE_BREAK
    }
    timerStates mState = timerStates.STATE_WORK;
    bool isPaused = true;

    // Timer components.
    public Button timerButton;
    public Text timerButtonText;
    public Text timerStateText;
    // Timer data.
    public float startTime_work = 10.0f;
    public float startTime_break = 10.0f;
    private float currentTime;
    private string[] timerStateStrings;

    void Start ()
    {
        // Init. strings.
        timerStateStrings = new string[2];
        timerStateStrings[(int)timerStates.STATE_WORK] = "WORK";
        timerStateStrings[(int)timerStates.STATE_BREAK] = "BREAK";
        // Init. timer misc.
        currentTime = startTime_work;
        // Init. text components.
        UpdateTimerText();
        timerStateText.text = timerStateStrings[(int)timerStates.STATE_WORK];
    }
	
	void Update ()
    {
        // Update timer.
        if (!isPaused)
        {
            // Update current time.
            currentTime -= Time.deltaTime;
            // Update timer text.
            UpdateTimerText();
            // Watch timer tick.
            if (currentTime <= 0)
                AtTimerEnd();
        }
    }

    void AtTimerEnd()
    {
        // Init. new state.
        if (mState == timerStates.STATE_WORK)
        {
            // Switch to break-state.
            mState = timerStates.STATE_BREAK;
            currentTime = startTime_break;
        }
        else
        {
            // Switch to work-state.
            mState = timerStates.STATE_WORK;
            currentTime = startTime_work;

        }
        // Update UI text.
        UpdateTimerText();
        timerStateText.text = timerStateStrings[(int)mState];
        // Pause timer.
        OnButtonClick_startStop();
    }

    void UpdateTimerText()
    {
        string m, s, t;
        m = ((int)(currentTime / 60)).ToString();
        int fs = (int)Math.Ceiling(currentTime) % 60;
        s = fs.ToString();
        t = m + ":";
        if (fs < 10)
            t += "0";
        t += s;
        timerButtonText.text = t;
    }

    public void OnButtonClick_startStop()
    {
        isPaused = !isPaused;
    }
}
