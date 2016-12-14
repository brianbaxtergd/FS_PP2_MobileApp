using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WedgeScript : MonoBehaviour
{
    // Info.
    /*
        Holds data for wedges (which represent projects stored in DataController script in Application Controller.
    */

    public int index;
    public float angle;
    public float radius;
    public Color color;
    private GameObject applicationController;

    // Wedge prefab initializer.
    public void InitWedgePrefab(int _index, float _angle, float _radius, Color _color)
    {
        index = _index;
        angle = _angle;
        radius = _radius;
        color = _color;
    }

	// Use this for initialization
	void Start ()
    {
        applicationController = GameObject.Find("ApplicationController"); // Must find specific instance of prefab by name, or else project data is not accessible.
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void OnButtonClick()
    {
        // Update Data Controller's currently selected project index.
        DataController dc = applicationController.GetComponent<DataController>();
        dc.CurrentProjectIndex = index;
        // Call Main Canvas's Application State Machine Script method for opening a project.
        Canvas c = FindObjectOfType<Canvas>();
        ApplicationStateMachineScript appScript = c.GetComponent<ApplicationStateMachineScript>();
        appScript.OnClick_ProjectButton();
    }
}
