using UnityEngine;
using System.Collections;

public class Task /*: ScriptableObject*/
{
    // Data members.
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
    // Sets all initial values as an overloaded constructor would.
    public void Init(int _priority, string _name)
    {
        priority = (taskPriorities)_priority;
        taskName = _name;
    }
    // Unity methods.
    void Start ()
    {
	
	}
	void Update ()
    {
	
	}
}
