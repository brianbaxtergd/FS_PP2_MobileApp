using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProjectScreenController : MonoBehaviour{
    public GameObject ac;
    DataController dc;
    Project project;
    public GameObject tasks;
    public TaskController[] tasksArr;
    public Scrollbar scrollBar;
    int taskCount;
    float scrollSize;
    //int scrollPosition;
    // Use this for initialization
    void Start () {
        dc = ac.GetComponent<DataController>();
        project = (Project)dc.mProjectList[dc.CurrentProjectIndex];
        tasksArr = tasks.GetComponentsInChildren<TaskController>();
        //scrollPosition = 1;
        Draw();
    }

    // Update is called once per frame

    void Update () {
        project = (Project)dc.mProjectList[dc.CurrentProjectIndex];
        Draw();
    }
    void Draw () {
        taskCount = project.GetActiveTaskCount();
        scrollSize = (taskCount) / 9;
        if (taskCount % 9 > 0)
            scrollSize++;
        scrollBar.size = 1 / scrollSize; 
        int modifier = (int)(scrollBar.value * scrollSize);
        for (int i = 0; i < tasksArr.Length; i++)
        {
            tasksArr[i].gameObject.SetActive(true);
            if (project.GetActiveTask(i + modifier).GetName() == "EmptyTask")
                tasksArr[i].gameObject.SetActive(false);
            else
                tasksArr[i].SetName(project.GetActiveTask(i + modifier).GetName());
        }
    }
}
