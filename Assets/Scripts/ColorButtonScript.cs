using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorButtonScript : MonoBehaviour
{
    Image panelImage;

	// Use this for initialization
	void Start ()
    {
        panelImage = GameObject.Find("AddProjectPanel").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void OnClick_UpdatePanelColor()
    {
        panelImage.color = GetComponent<Image>().color;
    }
}
