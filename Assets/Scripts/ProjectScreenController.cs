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
    //int scrollPosition;
    // Use this for initialization
    void Start () {
        dc = ac.GetComponent<DataController>();
        project = (Project)dc.mProjectList[dc.CurrentProjectIndex];
        //scrollPosition = 1;
    }

    // Update is called once per frame

    void Update () {
        project = (Project)dc.mProjectList[dc.CurrentProjectIndex];
        Draw();
    }
    void DeleteTaskList () {
        foreach (Transform child in tasksParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
    public void Draw () {
        DeleteTaskList();
        if (project.GetActiveTaskCount() > 0)
        {
            for (int i = 0; i < project.GetActiveTaskCount(); i++)
            {
                GameObject go = Instantiate(taskPrefab) as GameObject;
                go.GetComponent<TaskController>().SetName(project.GetActiveTask(i).GetName());
                go.transform.parent = tasksParent.transform;
            }
        }
    }
}
