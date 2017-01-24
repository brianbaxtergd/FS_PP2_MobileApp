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
    private ApplicationStateMachineScript asmc;
    // Resources.
    public AudioClip sndTimerWarning;
    public AudioClip sndTimerEnd;
    //GameOBject To Play sound on Click.
    public AudioSource OkSound;
    public AudioSource BackSound;
    // Messages.
    string[] breakMessages;
    string[] workMessages;
    bool showMessage = false;
    float messageFadeTimer_cur = 0;
    float messageFadeTimer_max = 1.5f;
    // Timer data.
    private float mCurrentTime;
    private float mStartTime_work = 25 * 60; // 25 * 60;
    private float mStartTime_break = 5 * 60; // 5 * 60;
    public float mWarningTime;
    private string[] mTimerStates;

	// Methods.
	void Start ()
    {
        // Initialize data structures.
        InitStrings();
        // Store script component(s).
        mPanelScript = GetComponent<QuickTimerPanelScript>();
        asmc = GameObject.Find("MainCanvas").GetComponent<ApplicationStateMachineScript>();
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
        workMessages[0] = "Time to crush tasks and chew bubble gum\n..and you're all out of gum.";
        workMessages[1] = "Start where you are.\nUse what you have.\nDo what you can.";
        workMessages[2] = "Let's go, champ!";
        breakMessages = new string[3];
        breakMessages[0] = "Well done.";
        breakMessages[1] = "That's enough for now. Go relax!";
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
        int fs = (int)Math.Floor(mCurrentTime) % 60; // ceiling
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
        if (asmc.GetTimerAudio())
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
        if (asmc.GetTimerMessages())
        {
            RandomizeMessage();
            showMessage = true;
        }
        // Pause timer.
        OnButtonClick_startStop();
    }
	// Update is called once per frame
	void Update ()
    {
        if (asmc.GetTimerMessages())
            UpdateMessage();
        else
        {
            mEndMessageText.text = "";
        }

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
                if (GameObject.Find("MainCanvas").GetComponent<ApplicationStateMachineScript>().GetTimerAudio())
                    audioSource.PlayOneShot(sndTimerWarning);
            }
        }
	}
    public void OnButtonClick_startStop()
    {
        mPaused = !mPaused;
        // Update button text.
        if (mPaused)
        {
            mTimerButtonText.text = "START";
            BackSound.Play();
        }
        else
        {
            mTimerButtonText.text = "PAUSE";
            if (showMessage)
                showMessage = false;
            OkSound.Play();
        }
    }
}
