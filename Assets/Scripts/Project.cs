using UnityEngine;
using System.Collections;

public class Project
{
    // Data.
    string mName;
    Color mColor;
    ArrayList mTaskList = new ArrayList();
    ArrayList mArchivedTaskList = new ArrayList();

    // Constructors.
    public Project()
    {
        mName = "Default Project";
        Color mColor = Color.yellow;
        // Adds a blank task to every new project.
        mTaskList.Add(new Task());
    }
    public Project(string _name, Color _color)
    {
        mName = _name;
        mColor = _color;
        // Adds a blank task to every new project.
        mTaskList.Add(new Task());
    }
    // Tasklist interface.
    public int GetActiveTaskCount()
    {
        return mTaskList.Count;
    }
    public int GetArchivedTaskCount()
    {
        return mArchivedTaskList.Count;
    }
    public int GetTotalTaskCount()
    {
        return mTaskList.Count + mArchivedTaskList.Count;
    }
    public Color GetColor()
    {
        return mColor;
    }
    public void AddTask(string _taskName, int _priority)
    {
        Task t = new Task(_taskName, _priority);
        mTaskList.Add(t);
    }
    public void CompleteTask(int _index)
    {
        if (_index >= 0 && _index < mTaskList.Count)
        {
            mArchivedTaskList.Add(mTaskList[_index]);
            mTaskList.RemoveAt(_index);
        }
    }
    public void DeleteTask(int _index)
    {
        if (_index >= 0 && _index < mTaskList.Count)
            mTaskList.RemoveAt(_index);
    }
}
