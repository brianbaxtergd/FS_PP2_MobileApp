using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataController : MonoBehaviour
{
    // Add New Project Panel Components.
    public InputField mProjectNameInputField;
    public InputField mTaskNameInputField;

    public GameObject addProjectPanel;

    // Other components.
    public GameObject projectsGraph;
    private ProjectsGraphScript projectsGraphScript;

    // Data.
    public ArrayList mProjectList = new ArrayList();
    private int mCurrentProjectIndex = -1;

    // Unity methods.
    void Start()
    {
        projectsGraphScript = projectsGraph.GetComponent<ProjectsGraphScript>();
    }

    public int GetProjectCount()
    {
        return mProjectList.Count;
    }

    // Creates project from panel component data.
    public void AddProject()
    {
        Project p = new Project(mProjectNameInputField.textComponent.text, addProjectPanel.GetComponent<Image>().color);
        mProjectList.Add(p);
        // Update projects graph.
        projectsGraphScript.UpdateGraph();
    }
    public void AddTask()
    {
        Task t = new Task(mTaskNameInputField.textComponent.text, 0);
        ((Project)mProjectList[mCurrentProjectIndex]).AddTask(mTaskNameInputField.textComponent.text, 0);
    }

    // Property to get/set current project index.
    public int CurrentProjectIndex
    {
        get
        {
            return mCurrentProjectIndex;
        }
        set
        {
            mCurrentProjectIndex = value;
        }
    }

    public void RemoveProject(int _index)
    {
        mProjectList.RemoveAt(_index);
    }

    public void OpenProject(int _index)
    {
        if (_index > -1 && _index < mProjectList.Count)
        {
            //mCurrentProjectIndex = _index;
            //SceneManager.LoadScene("ProjectTasks");
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

    public int GetTotalTaskCountInAllProjects()
    {
        int totalTaskCount = 0;
        for (int i = 0; i < mProjectList.Count; i++)
        {
            Project p = mProjectList[i] as Project;
            totalTaskCount += p.GetTotalTaskCount();
        }
        return totalTaskCount;
    }
}
