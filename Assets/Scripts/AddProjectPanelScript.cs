using UnityEngine;
using UnityEngine.UI;

using System.Collections;

// Info.
/*
    
*/

public class AddProjectPanelScript : MonoBehaviour
{
    public InputField nameInputField;
    public Text nameText;
    Image colorImage;
    public Button okButton;

	// Use this for initialization
	void Start ()
    {
        colorImage = GetComponent<Image>();
	}
	
	// Update is called once per frame
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

    // The following (2) methods are not used. See ColorButtonScript which bypasses these methods.
    public Color GetPanelColor()
    {
        Image img = GetComponent<Image>();
        return img.color;
    }
    public void SetPanelColor(Color _col)
    {
        // Get Image-component within panel.
        Image img = GetComponent<Image>();
        // Apply new color to Image-component, updating the panel's background color.
        img.color = _col;
    }
}
