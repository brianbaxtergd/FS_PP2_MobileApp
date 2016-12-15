using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuickTimerPanelScript : MonoBehaviour
{
    // Info:
    /*
        Color-scheme fade between states.
    */

    // Data.
    public Color bgCol_work;
    public Color fgCol_work;
    public Color bgCol_break;
    public Color fgCol_break;
    bool colorFadeActive = false;
    bool isWorkState = true;
    float fadeTimer_cur = 0.0f;
    public float fadeTimer_max;
    Image img;
    public Text timerValueText;
    public Text timerStartPauseButtonText;
    public Text timerStateText;
    public GameObject backButton;
    public GameObject startStopButton; // Text stored in above data member.

    // Unity Methods.
    void Start()
    {
        img = GetComponent<Image>();

        // Testing fade.
        //SetColorFade(true);
    }
    void Update()
    {
        // Color fading.
        if (colorFadeActive)
            UpdateColors();
    }

    // Methods.
    public bool GetColorFade()
    {
        return colorFadeActive;
    }
    public void SetColorFade(bool _isActive)
    {
        colorFadeActive = _isActive;
    }
    private void UpdateColors()
    {
        // Update timer.
        fadeTimer_cur += Time.deltaTime;
        if (fadeTimer_cur >= fadeTimer_max)
        {
            // Reset timer data.
            colorFadeActive = false;
            fadeTimer_cur = 0;
            // Flip timer state.
            isWorkState = !isWorkState;
            // Snap final colors.
            if (isWorkState)
            {
                img.color = bgCol_work;
                timerValueText.color = fgCol_work;
                timerStateText.color = fgCol_work;
                Color c = fgCol_work;
                c.a = 0.5f;
                backButton.GetComponent<Image>().color = c;
                startStopButton.GetComponent<Image>().color = c;
                backButton.transform.Find("Text").GetComponent<Text>().color = bgCol_work;
                startStopButton.transform.Find("Text").GetComponent<Text>().color = bgCol_work;
            }
            else
            {
                img.color = bgCol_break;
                timerValueText.color = fgCol_break;
                timerStateText.color = fgCol_break;
                Color c = fgCol_break;
                c.a = 0.5f;
                backButton.GetComponent<Image>().color = c;
                startStopButton.GetComponent<Image>().color = c;
                backButton.transform.Find("Text").GetComponent<Text>().color = bgCol_break;
                startStopButton.transform.Find("Text").GetComponent<Text>().color = bgCol_break;
            }
            // Exit Update() early.
            return;
        }
        // Apply change to colors.
        float t = (fadeTimer_cur / fadeTimer_max);
        if (isWorkState)
        {
            // Fade colors toward break-state.
            img.color = Color.Lerp(bgCol_work, bgCol_break, t);
            timerValueText.color = Color.Lerp(fgCol_work, fgCol_break, t);
            timerStateText.color = Color.Lerp(fgCol_work, fgCol_break, t);
            // Buttons.
            Color c = Color.Lerp(fgCol_work, fgCol_break, t);
            c.a = 0.5f;
            backButton.GetComponent<Image>().color = c;
            startStopButton.GetComponent<Image>().color = c;
            c = Color.Lerp(bgCol_work, bgCol_break, t);
            backButton.transform.Find("Text").GetComponent<Text>().color = c;
            startStopButton.transform.Find("Text").GetComponent<Text>().color = c;
        }
        else
        {
            // Fade colors toward work-state.
            img.color = Color.Lerp(bgCol_break, bgCol_work, t);
            timerValueText.color = Color.Lerp(fgCol_break, fgCol_work, t);
            timerStateText.color = Color.Lerp(fgCol_break, fgCol_work, t);
            // Buttons.
            Color c = Color.Lerp(fgCol_break, fgCol_work, t);
            c.a = 0.5f;
            backButton.GetComponent<Image>().color = c;
            startStopButton.GetComponent<Image>().color = c;
            c = Color.Lerp(bgCol_break, bgCol_work, t);
            backButton.transform.Find("Text").GetComponent<Text>().color = c;
            startStopButton.transform.Find("Text").GetComponent<Text>().color = c;
        }
    }
}
