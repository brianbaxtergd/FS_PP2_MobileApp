using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour
{
    // Data.
    private ArrayList mProjectList;
    private int mCurrentProjectIndex;

    // Unity.
	void Start ()
    {
        // Initialize project list.
        mProjectList = new ArrayList();
        mCurrentProjectIndex = -1;
	}
	void Update ()
    {
	    
	}
    // Project list interface.
    public int GetProjectCount()
    {
        return mProjectList.Count;
    }
    public void AddProject(string _name, Color _col)
    {
        Project p = new Project(_name, _col);
        mProjectList.Add(p);
        OpenProject(mProjectList.Count - 1);
    }
    public void RemoveProject(int _index)
    {
        mProjectList.RemoveAt(_index);
    }
    public void OpenProject(int _index)
    {
        if (_index > -1 && _index < mProjectList.Count)
        {
            mCurrentProjectIndex = _index;
            SceneManager.LoadScene("ProjectTasks");
        }
    }
    // Save/Load data from local device.
    public bool LoadData(string _path)
    {
        return false;
    }

    public bool SaveData(string _path)
    {
        return false;
    }
}
