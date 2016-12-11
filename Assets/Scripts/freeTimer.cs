using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

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
    public Text mTimerText;
    public Button mTimerButton;
    public Text mTimerButtonText;
    public Text mTimerStateText;
    public Text mEndMessageText;
    public Canvas mCanvas;
    public AudioSource audioSource;
    private QuickTimerPanelScript mPanelScript; // Does this find the script in the Panel child-obj?
    // Resources.
    public AudioClip sndTimerWarning;
    public AudioClip sndTimerEnd;
    // Messages.
    string[] breakMessages;
    string[] workMessages;
    bool showMessage = false;
    float messageFadeTimer_cur = 0;
    float messageFadeTimer_max = 1.5f;
    // Timer data.
    private float mCurrentTime;
    private float mStartTime_work = 10; // 25 * 60;
    private float mStartTime_break = 10; // 5 * 60;
    public float mWarningTime;
    private string[] mTimerStates;

	// Methods.
	void Start ()
    {
        // Initialize data structures.
        InitStrings();
        // Store script component(s).
        mPanelScript = GetComponent<QuickTimerPanelScript>();
        // Prep initial timer-state.
        mState = timerStates.STATE_WORK;
        mTimerStateText.text = mTimerStates[(int)timerStates.STATE_WORK];
        mPaused = true;
        mCurrentTime = mStartTime_work;
        UpdateTimerText();
        mEndMessageText.text = "";
    }
    void InitStrings()
    {
        // Timer state.
        mTimerStates = new string[2];
        mTimerStates[(int)timerStates.STATE_WORK] = "WORK";
        mTimerStates[(int)timerStates.STATE_BREAK] = "BREAK";
        // Messages.
        workMessages = new string[3];
        workMessages[0] = "It's time to crush tasks and chew bubblegum.. and you're all out of gum.";
        workMessages[1] = "Start where you are. Use what you have. Do what you can.";
        workMessages[2] = "Back for more? Let's get to it!";
        breakMessages = new string[3];
        breakMessages[0] = "Well done! Take a breather and put your mind at ease.";
        breakMessages[1] = "Time for a snack, you workaholic.";
        breakMessages[2] = "Time for a break. You earned it!";
    }
    void UpdateMessage()
    {
        // Fade text alpha.

        // Update fade timer.
        if (showMessage)
        {
            if (messageFadeTimer_cur < messageFadeTimer_max)
                messageFadeTimer_cur += Time.deltaTime;
            else
                messageFadeTimer_cur = messageFadeTimer_max;
        }
        else
        {
            if (messageFadeTimer_cur > 0)
                messageFadeTimer_cur -= Time.deltaTime;
            else
                messageFadeTimer_cur = 0;
        }
        // Update text alpha.
        Color tmpCol = mEndMessageText.color;
        tmpCol.a = messageFadeTimer_cur / messageFadeTimer_max;
        mEndMessageText.color = tmpCol;
    }
    void RandomizeMessage()
    {
        // Assign a random string to the messageText gameObject.
        System.Random r = new System.Random((int)Time.time);
        int i = r.Next(0, 3); // (0 - 2) Note: minVal is inclusive, maxVal is exclusive.
        if (mState == timerStates.STATE_WORK)
        {
            mEndMessageText.text = workMessages[i];
        }
        else // mState == timerStates.STATE_BREAK.
        {
            mEndMessageText.text = breakMessages[i];
        }
    }
    void UpdateTimerText()
    {
        string m, s, t;
        m = ((int)(mCurrentTime / 60)).ToString();
        int fs = (int)Math.Ceiling(mCurrentTime) % 60;
        s = fs.ToString();
        t = m + ":";
        if (fs < 10)
            t += "0";
        t += s;
        mTimerText.text = t;
    }
    void AtTimerEnd()
    {
        // Play audio.
        audioSource.PlayOneShot(sndTimerEnd);
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
        RandomizeMessage();
        showMessage = true;
        // Pause timer.
        OnButtonClick_startStop();
    }
	// Update is called once per frame
	void Update ()
    {
        UpdateMessage();

        if (!mPaused)
        {
            // Update current time.
            mCurrentTime -= Time.deltaTime;
            // Update timer text.
            UpdateTimerText();
            // Watch timer tick.
            if (mCurrentTime <= 0)
                AtTimerEnd();
            else if (mCurrentTime <= mWarningTime && !mPanelScript.GetColorFade())
            {
                // Trigger color fading in panel script.
                mPanelScript.SetColorFade(true);
                // Play warning audio as timer approaches end.
                audioSource.PlayOneShot(sndTimerWarning);
            }
        }
	}
    public void OnButtonClick_startStop()
    {
        mPaused = !mPaused;
        // Update button text.
        if (mPaused)
            mTimerButtonText.text = "START";
        else
        {
            mTimerButtonText.text = "PAUSE";
            if (showMessage)
                showMessage = false;
        }
    }
}
