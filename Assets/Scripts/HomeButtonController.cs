using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class HomeButtonController : MonoBehaviour {

    public void OnButtonClick()
    {
        SceneManager.LoadScene("Home");
    }

	// Unity.
	void Start ()
    {
	}
	void Update ()
    {
	}
}
