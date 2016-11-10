using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour
{
    // Data members.

    private ArrayList projects;
    private int currentProject;

    // Unity Behavior.

	void Start ()
    {
        // Initialize project list.
        projects = new ArrayList();
        currentProject = -1;
	}
	void Update ()
    {
	    
	}

    // Public interface.

    public int GetProjectCount()
    {
        return projects.Count;
    }

    public void AddProject(string _name, Color _col)
    {
        //project n = new Project(_name, _col);
        //projects.Add(n);
        OpenProject(projects.Count - 1);
    }

    public void RemoveProject(int _index)
    {
        projects.RemoveAt(_index);
    }

    public void OpenProject(int _index)
    {
        if (_index > -1 && _index < projects.Count)
        {
            currentProject = _index;
            SceneManager.LoadScene("ProjectTasks");
        }
    }

    public bool LoadData(string _path)
    {
        return false;
    }

    public bool SaveData(string _path)
    {
        return false;
    }
}
