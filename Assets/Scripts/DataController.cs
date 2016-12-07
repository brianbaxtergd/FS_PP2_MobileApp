using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LWProject : MonoBehaviour
{
    public int importance;
    public float completion;
    public Color color;
    public bool isReady;
    public LWProject() { isReady = false; }
    public LWProject(float _completion, Color _color, int _importance)
    {
        importance = _importance;
        completion = _completion;
        color = _color;
    }
}
/*static*/
public class DataController : MonoBehaviour
{
    [SerializeField]
    InputField mProjectNameInputField;

    // Data.
    private ArrayList mProjectList = new ArrayList();
    PieGraph mPieGraph;
    //private int mCurrentProjectIndex = -1;

    // Unity.
    void Awake()
    {
        // Force object persistence between scenes.
        DontDestroyOnLoad(transform.gameObject);
    }
    void Start()
    {
        mPieGraph = GetComponent<PieGraph>();
    }
    public LWProject GetWedge(int projectIndex)
    {
        LWProject lwp;
        if (projectIndex < mProjectList.Count)
        {
            Project p = (Project)mProjectList[projectIndex];
            lwp = new LWProject(p.GetActiveTaskCount(), p.GetColor(), p.GetTotalTaskCount());
            lwp.isReady = true;
            return lwp;
        }
        lwp = new LWProject();
        return lwp;
    }
    // Project list interface.
    public int GetProjectCount()
    {
        return mProjectList.Count;
    }
    public void AddProject()
    {
        // Grab text from UI component.

        System.Random rng = new System.Random();
        Color color = Color.yellow;
        color = new Color(rng.Next(255) * 0.00392f, rng.Next(255) * 0.00392f, rng.Next(255) * 0.00392f);
        Project p = new Project(mProjectNameInputField.textComponent.text, color);
        
        mProjectList.Add(p);
        OpenProject(mProjectList.Count - 1);
        //mPieGraph.Add(1, color, 1); <-- This was causing a null ref error.
    }
    void UpdateGraph()
    {
        mPieGraph.DestroyGraph();
        for (int i = 0; i < mProjectList.Count; i++)
        {
            Project p = (Project)mProjectList[i];
            mPieGraph.Add(p.GetActiveTaskCount(), p.GetColor(), p.GetTotalTaskCount());
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
    public void Test()
    {
        Project p = new Project("Test", Color.green);
        p.AddTask("task", 1);
        p.AddTask("task2", 1);
        p.AddTask("task3", 1);
        p.CompleteTask(1);
        mProjectList.Add(p);
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
}
