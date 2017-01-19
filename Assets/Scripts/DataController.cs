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
        LoadData();
    }
    void OnApplicationQuit()
    {
        // Save user-data.
        SaveData();
    }

    public int GetProjectCount()
    {
        return mProjectList.Count;
    }

    // Creates project from panel component data.
    public void AddProject()
    {
        Project p = new Project(mProjectNameInputField.textComponent.text, addProjectPanel.GetComponent<AddProjectPanelScript>().GetCurrentColorChoice());
        mProjectList.Add(p);
        // Update projects graph.
        projectsGraphScript.UpdateGraph();
    }
    public void AddTask()
    {
        //Task t = new Task(mTaskNameInputField.textComponent.text, 0);
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
    public void LoadData()
    {
        // Info.
        /*

        */

        // Cancel loading if user has yet to store data.
        if (!PlayerPrefs.HasKey("projectCount"))
            return;
        // Build projects from stored data.
        int projectCount = PlayerPrefs.GetInt("projectCount");
        //mProjectList.Capacity = projectCount;
        for (int i = 0; i < projectCount; i++)
        {
            // Build project, store name & color.
            Project p = new Project(
                PlayerPrefs.GetString("p" + i.ToString() + "_name"),
                new Color(
                    PlayerPrefs.GetFloat("p" + i.ToString() + "_colorRed"),
                    PlayerPrefs.GetFloat("p" + i.ToString() + "_colorGreen"),
                    PlayerPrefs.GetFloat("p" + i.ToString() + "_colorBlue"),
                    PlayerPrefs.GetFloat("p" + i.ToString() + "_colorAlpha")
                    )
                );
            // Build project's active tasklist.
            int activeTaskCount = PlayerPrefs.GetInt("p" + i.ToString() + "_activeTaskCount");
            //p.mTaskList.Capacity = activeTaskCount;
            for (int k = 0; k < activeTaskCount; k++)
            {
                // Initialize active tasks.
                Task t = new Task(
                    PlayerPrefs.GetString("p" + i.ToString() + "_activeTask" + k.ToString() + "_name"),
                    1
                    );
                p.mTaskList.Add(t);
            }
            // Build project's archived tasklist.
            int archivedTaskCount = PlayerPrefs.GetInt("p" + i.ToString() + "_archivedTaskCount");
            //p.mArchivedTaskList.Capacity = activeTaskCount;
            for (int k = 0; k < archivedTaskCount; k++)
            {
                // Initialize archived tasks.
                Task t = new Task(
                    PlayerPrefs.GetString("p" + i.ToString() + "_activeTask" + k.ToString() + "_name"),
                    1
                    );
                p.mArchivedTaskList.Add(t);
            }
            mProjectList.Add(p);
        }
        // Force projects graph to update.
        projectsGraphScript.UpdateGraph();
    }

    public void SaveData()
    {
        // Info.
        /*
            Purpose:
            Clear previously stored user-data, then store current user-data.

            Keys:
            projectCount
            p0_name, where '0' is the project's index.
            p0_colorRed
            p0_colorGreen
            p0_colorBlue
            p0_colorAlpha
            p0_activeTaskCount
            p0_activeTask0_name, where '0' following "activeTask" is the task's index in mTaskList
            p0_archivedTaskCount
            p0_archivedTask0_name, where '0' following "archivedTask" is the task's index in mArchivedTaskList
        */

        // Clear previously stored data.
        PlayerPrefs.DeleteAll();
        // Store project count.
        PlayerPrefs.SetInt("projectCount", mProjectList.Count);
        // Iterate through all projects, storing current user-data.
        for (int i = 0; i < mProjectList.Count; i++)
        {
            string key;
            Project p = mProjectList[i] as Project;
            // Store project name.
            key = "p" + i.ToString() + "_name";
            PlayerPrefs.SetString(key, p.Name);
            // Store project color.
            key = "p" + i.ToString() + "_colorRed";
            PlayerPrefs.SetFloat(key, p.GetColor().r);
            key = "p" + i.ToString() + "_colorGreen";
            PlayerPrefs.SetFloat(key, p.GetColor().g);
            key = "p" + i.ToString() + "_colorBlue";
            PlayerPrefs.SetFloat(key, p.GetColor().b);
            key = "p" + i.ToString() + "_colorAlpha";
            PlayerPrefs.SetFloat(key, p.GetColor().a);
            // Store active task count.
            key = "p" + i.ToString() + "_activeTaskCount";
            PlayerPrefs.SetInt(key, p.mTaskList.Count);
            // Store active task data.
            for (int k = 0; k < p.mTaskList.Count; k++)
            {
                // Store each active task's name.
                Task t = p.mTaskList[k] as Task;
                key = "p" + i.ToString() + "_activeTask" + k.ToString() + "_name";
                PlayerPrefs.SetString(key, t.GetName());
            }
            // Store archived task count.
            key = "p" + i.ToString() + "_archivedTaskCount";
            PlayerPrefs.SetInt(key, p.mArchivedTaskList.Count);
            // Store archived task data.
            for (int k = 0; k < p.mArchivedTaskList.Count; k++)
            {
                // Store each archived task's name.
                Task t = p.mArchivedTaskList[k] as Task;
                key = "p" + i.ToString() + "_archivedTask" + k.ToString() + "_name";
                PlayerPrefs.SetString(key, t.GetName());
            }
        }
        // Write data to disk/device.
        PlayerPrefs.Save();
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
    public int Wedge_GetTotalTaskCountInAllProjects()
    {
        int totalTaskCount = 0;
        for (int i = 0; i < mProjectList.Count; i++)
        {
            Project p = mProjectList[i] as Project;
            int taskCount = p.GetTotalTaskCount();
            if (taskCount <= 0)
                taskCount = 1;
            totalTaskCount += taskCount;
        }
        return totalTaskCount;
    }
}
