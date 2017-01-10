using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProjectScreenController : MonoBehaviour{
    public GameObject ac;
    DataController dc;
    Project project;
    public GameObject task1;
    public GameObject task2;
    public GameObject task3;
    public GameObject task4;
    public GameObject task5;
    public GameObject task6;
    public GameObject task7;
    public GameObject task8;
    public GameObject task9;
    int taskCount;
    float scrollSize;
    //int scrollPosition;
    // Use this for initialization
    void Start () {
        dc = ac.GetComponent<DataController>();
        project = (Project)dc.mProjectList[dc.CurrentProjectIndex];
        //scrollPosition = 1;
        Draw();
    }

    // Update is called once per frame

    void Update () {
        project = (Project)dc.mProjectList[dc.CurrentProjectIndex];
    }
    void Draw () {
        taskCount = project.GetActiveTaskCount();
        scrollSize = taskCount / 11;
        //int multiplier = 0;
        
        for (int i = 0; i < taskCount; i++)
        {


        }
    }
}
