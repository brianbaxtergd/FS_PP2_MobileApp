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
                //timerStartPauseButtonText.color = fgCol_work;
            }
            else
            {
                img.color = bgCol_break;
                timerValueText.color = fgCol_break;
                timerStateText.color = fgCol_break;
                //timerStartPauseButtonText.color = fgCol_break;
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
            //timerStartPauseButtonText.color = Color.Lerp(fgCol_work, fgCol_break, t);
        }
        else
        {
            // Fade colors toward work-state.
            img.color = Color.Lerp(bgCol_break, bgCol_work, t);
            timerValueText.color = Color.Lerp(fgCol_break, fgCol_work, t);
            timerStateText.color = Color.Lerp(fgCol_break, fgCol_work, t);
            //timerStartPauseButtonText.color = Color.Lerp(fgCol_break, fgCol_work, t);
        }
    }

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

}
