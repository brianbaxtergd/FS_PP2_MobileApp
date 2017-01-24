using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleScript : MonoBehaviour {
    public GameObject ac;
    public GameObject projectView;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnPointerClick()
    {
        ac.GetComponent<DataController>().ToggleTaskState();
        projectView.GetComponent<ProjectScreenController>().Draw();
    }
}
