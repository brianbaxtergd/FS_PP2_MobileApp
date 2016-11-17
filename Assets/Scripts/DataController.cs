using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

static public class DataController
{
    // Data.
    static private ArrayList mProjectList = new ArrayList();
    static private int mCurrentProjectIndex = -1;

    // Unity.
    static public Wedge GetWedge(int projectIndex)
    {
        Wedge newWedge;
        if (projectIndex < mProjectList.Count)
        {
            Project p = (Project)mProjectList[projectIndex];
            newWedge = new Wedge(p.GetActiveTaskCount(), p.GetColor(), p.GetTotalTaskCount());
            newWedge.isReady = true;
            return newWedge;
        }
        newWedge = new Wedge();
        newWedge.isReady = false;
        return newWedge;
    }
    // Project list interface.
    static public int GetProjectCount()
    {
        return mProjectList.Count;
    }
    static public void AddProject(string _name, Color _col)
    {
        Project p = new Project(_name, _col);
        mProjectList.Add(p);
        OpenProject(mProjectList.Count - 1);
    }
    static public void RemoveProject(int _index)
    {
        mProjectList.RemoveAt(_index);
    }
    static public void OpenProject(int _index)
    {
        if (_index > -1 && _index < mProjectList.Count)
        {
            mCurrentProjectIndex = _index;
            SceneManager.LoadScene("ProjectTasks");
        }
    }
    static public void Test()
    {
        Project p = new Project("Test", Color.green);
        p.AddTask("task", 1);
        p.AddTask("task2", 1);
        p.AddTask("task3", 1);
        p.CompleteTask(1);
        mProjectList.Add(p);
    }
    // Save/Load data from local device.
    static public bool LoadData(string _path)
    {
        return false;
    }

    static public bool SaveData(string _path)
    {
        return false;
    }
}
