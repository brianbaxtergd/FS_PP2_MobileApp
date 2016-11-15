using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class HomeButtonController : MonoBehaviour {

    [SerializeField]
    Button mHomeButton;
    [SerializeField]
    Scene mHomeScene;

    public void OnButtonClick()
    {
        SceneManager.LoadScene(0);
    }

	// Unity.
	void Start ()
    {
	}
	void Update ()
    {
	}
}
