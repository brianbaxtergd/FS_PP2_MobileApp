using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartQuickTimer : MonoBehaviour {

    public void OnButtonClick_startQuickTimer()
    {
        SceneManager.LoadScene("FreeTimer");
    }

	// Use this for initialization
	void Start()
    {
	
	}
	
	// Update is called once per frame
	void Update() {
	
	}
}
