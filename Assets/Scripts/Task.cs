using UnityEngine;
using System.Collections;

public class Task : ScriptableObject
{
    enum taskPriorities
    {
        LOW,
        NORM,
        HIGH
    }
    taskPriorities priority;
    string taskName;

    public Task()
    {
        taskName = "Default Task";
        priority = taskPriorities.NORM;
    }

    public Task(string _taskName, int _priority)
    {
        taskName = _taskName;
        priority = (taskPriorities)_priority;
    }

    void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}
}
