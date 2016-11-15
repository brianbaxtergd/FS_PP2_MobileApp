using UnityEngine;
using System.Collections;

public class Project : MonoBehaviour
{
    // Data.
    string mName;
    Color mColor;
    ArrayList mTaskList;
    ArrayList mArchivedTaskList;

    // Constructors.
    public Project()
    {
        mName = "Default Project";
        Color mColor = Color.white;
        mTaskList = new ArrayList();
        mArchivedTaskList = new ArrayList();
    }
    public Project(string _name, Color _color)
    {
        mName = _name;
        mColor = _color;
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
    public void AddTask(string _taskName, int _priority)
    {
        Task t = new Task(_taskName, _priority);
        mTaskList.Insert(0, t);
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
    // Unity function.
    void Start()
    {

    }
    void Update()
    {

    }
    // Rendering.
    public void RenderTaskList()
    {

    }
}
