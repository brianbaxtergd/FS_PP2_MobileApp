using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TaskListButtonController : MonoBehaviour {

    public void OnButtonClick()
    {
        SceneManager.LoadScene("TaskList");
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
