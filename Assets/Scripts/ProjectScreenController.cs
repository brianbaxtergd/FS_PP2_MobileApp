using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProjectScreenController : MonoBehaviour{
    public GameObject ac;
    DataController dc;
    Project project;
    public GameObject taskPrefab;
    public GameObject tasksParent;
	bool taskViewActive = true;
	public Button taskViewButton;
    bool updatingTaskView = false;
    //int scrollPosition;
    // Use this for initialization
    void Start () {
        dc = ac.GetComponent<DataController>();
        project = (Project)dc.mProjectList[dc.CurrentProjectIndex];
        //scrollPosition = 1;
        Update();
		taskViewActive = true;
        updatingTaskView = false;
        Draw();
    }
    public void Init()
    {
        Update();
        taskViewActive = true;
        UpdateTaskViewStateLabel();
        updatingTaskView = false;
        Draw();
    }
    // Update is called once per frame

    void Update () {
        //project = (Project)dc.mProjectList[dc.CurrentProjectIndex];
    }
    void DeleteTaskList () {
        foreach (Transform child in tasksParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
	public void ChangeTaskViewState () {
        //updatingTaskView = true;
        if (taskViewActive) {
			taskViewActive = false;
            taskViewButton.GetComponentInChildren<Text>().text = "VIEW ACTIVE";

        }
        else {
			taskViewActive = true;
            taskViewButton.GetComponentInChildren<Text>().text = "VIEW ARCHIVE";
		}

        Draw();
	}
    public void UpdateTaskViewStateLabel()
    {
        if (taskViewActive)
        {
            taskViewButton.GetComponentInChildren<Text>().text = "VIEW ARCHIVE";


        }
        else {
            taskViewButton.GetComponentInChildren<Text>().text = "VIEW ACTIVE";

        }
    }

    internal bool isUpdating()
    {
        return updatingTaskView;
    }

    public void Draw () {
		DeleteTaskList();
		project = (Project)dc.mProjectList[dc.CurrentProjectIndex];
		if (taskViewActive)
		{
			if (project.GetActiveTaskCount() > 0)
			{
				for (int i = 0; i < project.GetActiveTaskCount(); i++)
				{
					if (project.GetActiveTask(i).GetPriority() != -1)
					{
						GameObject go = Instantiate(taskPrefab) as GameObject;
						go.GetComponent<TaskController>().SetName(project.GetActiveTask(i).GetName());
						go.GetComponent<TaskController>().SetIndex(i);
                        go.transform.SetParent(tasksParent.transform);
					}
				}
			}
		}
		else
		{
			if (project.GetArchivedTaskCount() > 0)
			{
				for (int i = 0; i < project.GetArchivedTaskCount(); i++)
				{
					if (project.GetArchivedTask(i).GetPriority() != -1)
					{
						GameObject go = Instantiate(taskPrefab) as GameObject;
						go.GetComponent<TaskController>().SetName(project.GetArchivedTask(i).GetName());
						go.GetComponent<TaskController>().SetIndex(i);
                        //go.GetComponentInChildren<Toggle>().isOn = true;
                        //go.GetComponentInChildren<Toggle>().on = true;
                        go.transform.SetParent(tasksParent.transform);
					}
				}
			}
		}
        //if (updatingTaskView)
        //    updatingTaskView = false;
    }
}
