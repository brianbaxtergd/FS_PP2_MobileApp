using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorButtonScript : MonoBehaviour
{
    Image panelImage;
    AddProjectPanelScript panelScript;

	// Use this for initialization
	void Start ()
    {
        panelImage = GameObject.Find("AddProjectPanel").GetComponent<Image>();
        panelScript = GameObject.Find("AddProjectPanel").GetComponent<AddProjectPanelScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void OnClick_UpdatePanelColor()
    {
        // Invoke color-fading in AddProjectPanel's script.
        panelScript.SetCurrentColorChoice(GetComponent<Image>().color);

        // Pre-color fading implementation.
        //panelImage.color = GetComponent<Image>().color;
    }
}
