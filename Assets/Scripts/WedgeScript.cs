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
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
