using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WedgeScript : MonoBehaviour
{
    // Info.
    /*
        Holds data for wedges (which represent projects stored in DataController script in Application Controller.
    */

    // This struct represents a point in 2D space.
    struct Point
    {
        public float x, y;
        public Point(float _x, float _y)
        {
            x = _x;
            y = _y;
        }
    }

    public int index;
    public float angle;
    public float radius;
    public Color color;
    private GameObject applicationController;
    private GameObject canvas;

    //Play Ok Sound.
    private GameObject OkSound;

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
        canvas = GameObject.Find("MainCanvas");
        OkSound = GameObject.Find("OkSound");
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void OnButtonClick()
    {
        // Store center-point of graph & user's click-point.
            // Todo: Will these points of measurement compare as expected? Test using debugger.
        Point pCenter = new Point(transform.position.x, transform.position.y);
        Point pClick = new Point(Input.mousePosition.x, Input.mousePosition.y);
        // Build origin-vector & click-vector.
            // Todo: Ensure building vClick from user input correctly in order to compare with vOrigin.
        Vector2 vOrigin = new Vector2(0.0f, 1.0f); // Straight up.
        Vector2 vClick = new Vector2(pClick.x - pCenter.x, pClick.y - pCenter.y);
        // Find angle between origin & click vectors.
            // Note: Assumes 0-deg origin is up vector (0, 1), rotating CCW.
        float aClick = 0.0f;
        if (pClick.x > pCenter.x)
            aClick = 360.0f - Vector2.Angle(vOrigin, vClick);
        else
            aClick = Vector2.Angle(vOrigin, vClick);
        // Iterate through wedges to identify project.
        DataController dc = applicationController.GetComponent<DataController>();
        ProjectsGraphScript pgs = GameObject.Find("ProjectsGraph").GetComponent<ProjectsGraphScript>();
        float iAngle = 0.0f;
        foreach (GameObject item in pgs.wedges)
        {
            // Check if aClick is within each wedge's angular boundaries.
            WedgeScript ws = item.GetComponent<WedgeScript>();
            if (aClick > iAngle && aClick <= iAngle + ws.angle * 360.0f)
            {
                // Update Data Controller's currently selected project index.
                dc.CurrentProjectIndex = ws.index;
                break;
            }
            // Increment iAngle.
            iAngle += (ws.angle * 360.0f);
        }
        // Call Main Canvas's Application State Machine Script method for opening a project.
        //Canvas c = FindObjectOfType<Canvas>();
        ApplicationStateMachineScript appScript = canvas.GetComponent<ApplicationStateMachineScript>();
        appScript.OnClick_ProjectButton();

        //Play Ok Sound.
        OkSound.GetComponent<AudioSource>().Play();

    }
}
