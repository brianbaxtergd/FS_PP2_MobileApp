using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class freeTimer : MonoBehaviour
{
    // State data.
    enum timerStates
    {
        STATE_WORK,
        STATE_BREAK
    }
    timerStates mState;
    bool mPaused;
    // Components.
    [SerializeField]
    public Text mTimerText;
    [SerializeField]
    public Button mTimerButton;
    [SerializeField]
    public Text mTimerButtonText;
    [SerializeField]
    public Text mTimerStateText;
    // Timer data.
    private float mCurrentTime;
    private float mStartTime_work;
    private float mStartTime_break;
    private string[] mTimerStates;

	// Use this for initialization
	void Start ()
    {
        mStartTime_work = 60 * 25;// * 60;
        mStartTime_break = 60 * 5;// * 60;
        mTimerStates = new string[2];
        mTimerStates[(int)timerStates.STATE_WORK] = "WORK";
        mTimerStates[(int)timerStates.STATE_BREAK] = "BREAK";
        mState = timerStates.STATE_WORK;
        mTimerStateText.text = mTimerStates[(int)timerStates.STATE_WORK];
        mPaused = true;
        mCurrentTime = mStartTime_work;
        UpdateTimerText();
    }
    void UpdateTimerText()
    {
        string m, s, t;
        m = ((int)(mCurrentTime / 60)).ToString();
        int fs = (int)mCurrentTime % 60;
        s = fs.ToString();
        t = m + ":";
        if (fs < 10)
            t += "0";
        t += s;
        mTimerText.text = t;
    }
    void AtTimerEnd()
    {
        // Init. new state.
        if (mState == timerStates.STATE_WORK)
        {
            // Switch to break-state.
            mState = timerStates.STATE_BREAK;
            mCurrentTime = mStartTime_break;
        }
        else
        {
            // Switch to work-state.
            mState = timerStates.STATE_WORK;
            mCurrentTime = mStartTime_work;
        }
        // Update UI text.
        UpdateTimerText();
        mTimerStateText.text = mTimerStates[(int)mState];
        // Pause timer.
        OnButtonClick_startStop();
    }
	// Update is called once per frame
	void Update ()
    {
        if (!mPaused)
        {
            // Update current time.
            mCurrentTime -= Time.deltaTime;
            // Update timer text.
            UpdateTimerText();
            // Watch for timer to reach zero.
            if (mCurrentTime <= 0)
                AtTimerEnd();
        }
	}
    public void OnButtonClick_startStop()
    {
        mPaused = !mPaused;
        // Update button text.
        if (mPaused)
            mTimerButtonText.text = "START";
        else
            mTimerButtonText.text = "PAUSE";
    }
}
