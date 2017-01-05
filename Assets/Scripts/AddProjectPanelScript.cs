using UnityEngine;
using UnityEngine.UI;

using System.Collections;

// Info.
/*
    
*/

public class AddProjectPanelScript : MonoBehaviour
{
    // Project name data.
    public InputField nameInputField;
    public Text nameText;

    // Project color data.
    bool isFadingColor = false;
    Image colorImage;
    Color curProjectColor; // Most recent color selection made by user. Stored as project color on ok-click.
    Color prevProjectColor; // Previous color selection made by user. Used for fading to new color.
    float colorFadeTimer_cur = 0.0f;
    public float colorFadeTimer_max; // Set in inspector. Timer increments using delta time from 0 to max value.

    // Panel buttons (excluding color choices).
    public Button okButton;

    // Overridden Unity methods.
    void Start ()
    {
        colorImage = GetComponent<Image>();
	}
	void Update ()
    {
	    // Set OK-button's active-state contingent on user input.
            // The OK-button should only be active if the user has both entered a project name
            // and chosen a project color.
        if (nameText.text != ""
            && colorImage.color != Color.white)
        {
            // Enable the OK button.
            if (!okButton.IsActive())
                okButton.gameObject.SetActive(true);
        }
        else
        {
            // Disable the OK button.
            if (okButton.IsActive())
                okButton.gameObject.SetActive(false);
        }

        // Fade bg-image's color toward current color choice.
            // isFadingColor, curProjectColor & prevProjectColor are set within ColorButtonScript's OnClick.
        if (isFadingColor)
        {
            if (colorFadeTimer_cur < colorFadeTimer_max)
            {
                // Increment timer.
                colorFadeTimer_cur += Time.deltaTime;
                if (colorFadeTimer_cur > colorFadeTimer_max)
                    colorFadeTimer_cur = colorFadeTimer_max;
                // Apply change in color.
                float t = colorFadeTimer_cur / colorFadeTimer_max; // Lerp value between 0.0 & 1.0.
                colorImage.color = Color.Lerp(prevProjectColor, curProjectColor, t);
            }
            else
            {
                // Fading between colors has completed.
                // Reset to default values and disable fading.
                isFadingColor = false;
                colorFadeTimer_cur = 0.0f;
                colorImage.color = curProjectColor;
            }
        }
	}
    void OnEnable()
    {
        // Reset panel to default state.

        // Reset panel's image color (background).
        GetComponent<Image>().color = Color.white;
        // Clear the project name input field text.
        nameInputField.Select();
        nameInputField.text = "";
        // Disable the OK button.
        okButton.gameObject.SetActive(false);
    }

    // Public interface methods.
    public Color GetCurrentColorChoice()
    {
        return curProjectColor;
    }
    public void SetCurrentColorChoice(Color _col)
    {
        // Do nothing if user has selected current color again.
        if (_col == colorImage.color)
            return;
        // Activate color fading.
        isFadingColor = true;
        // Store previous color used for fading to new color.
        prevProjectColor = colorImage.color;
        // Store new current color.
        curProjectColor = _col;
    }

    // Private interface methods.
}
