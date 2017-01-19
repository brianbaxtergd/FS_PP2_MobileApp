using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorButtonScript : MonoBehaviour
{
    Image panelImage;
    AddProjectPanelScript panelScript;
    Text buttonText;

	// Use this for initialization
	void Start ()
    {
        panelImage = GameObject.Find("AddProjectPanel").GetComponent<Image>();
        panelScript = GameObject.Find("AddProjectPanel").GetComponent<AddProjectPanelScript>();
        buttonText = this.gameObject.transform.GetChild(0).GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void OnClick_UpdatePanelColor()
    {
        // Invoke color-fading in AddProjectPanel's script.
        panelScript.SetCurrentColorChoice(GetComponent<Image>().color);
        // Clear all text from colored buttons.
        panelScript.ColoredButtons_ClearText();
        // Update text to show currently selected button.
        buttonText.text = "+";

        // Pre-color fading implementation.
        //panelImage.color = GetComponent<Image>().color;
    }
}
