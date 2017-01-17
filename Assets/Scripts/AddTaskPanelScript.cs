using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddTaskPanelScript : MonoBehaviour
{
    public InputField inputField;
    public string text;
    public Button ok;
    public GameObject ac;
    DataController dc;
    // Use this for initialization
    void Start()
    {
        dc = ac.GetComponent<DataController>();
    }
    // Update is called once per frame
    void Update()
    {
        text = inputField.textComponent.text;
    }
    public void OkPressed()
    {

    }
}
