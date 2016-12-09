using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuickTimerPanelScript : MonoBehaviour
{
    // Data.
    Color bgCol_work = Color.red;
    Color fgCol_work = Color.white;
    Color bgCol_break = Color.blue;
    Color fgCol_break = Color.cyan;
    bool colorFadeActive = false;
    bool isWorkState = true;
    float fadeTimer_cur = 0.0f;
    float fadeTimer_max = 10.0f;
    Image img;

    // Methods.
    public void SetColorFade(bool _isActive)
    {
        colorFadeActive = _isActive;
    }

    // Unity Methods.
    void Start()
    {
        img = GetComponent<Image>();
        SetColorFade(true);
    }
    void Update()
    {
        // Color fading.
        if (colorFadeActive)
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
                }
                else
                {
                    img.color = bgCol_break;
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
            }
            else
            {
                // Fade colors toward work-state.
                img.color = Color.Lerp(bgCol_break, bgCol_work, t);
            }
        }
    }
}
