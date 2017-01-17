using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddTaskPanelScript : MonoBehaviour
{
    public InputField inputField;
    public Text inputFieldText;
    public string text;
    public Button ok;
    public GameObject ac;
    DataController dc;

    void Start()
    {
        dc = ac.GetComponent<DataController>();
    }
    void OnEnable()
    {
        // Reset panel to default state.

        // Disable the OK button.
        ok.gameObject.SetActive(false);
        // Clear the project name input field text.
        inputField.Select();
        inputField.text = "";
    }
    void Update()
    {
        // Update local string "text" with whatever user has entered.
        text = inputField.textComponent.text;

        // Set OK-button's active-state contingent on user input.
            // OK-button is active once user has provided values for all fields.
        if (inputFieldText.text != "")
        {
            // Enable the OK button.
            if (!ok.IsActive())
                ok.gameObject.SetActive(true);
        }
        else
        {
            // Disable the OK button.
            if (ok.IsActive())
                ok.gameObject.SetActive(false);
        }
    }
    public void OkPressed()
    {

    }
}
