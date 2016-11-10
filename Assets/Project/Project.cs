using UnityEngine;
using System.Collections;


public class Project : MonoBehaviour
{
    string ProjectName; //The Name that the project is going to have.
    Color ProjectColor; //The color that the project is going to have on the Pie Graph.

    ArrayList UndoneTasks; //ArrayList of Uncomplite Tasks.
    ArrayList ArchiveTasks; //ArrayList of Complite Tasks.
 
    Project()
    {
        ProjectName = "New Project";
        ProjectColor = Color.blue;
    }

    Project(string name, Color color) //Overload constrocture.
    {
        ProjectName = name;
        ProjectColor = color;
    }

    public int TotalTaks() //Number of the total of tasks in the project.
    {
        return UndoneTasks.Count + ArchiveTasks.Count;
    }

    public int NumUndoneTasks() //Number of Undone tasks.
    {
        return UndoneTasks.Count;
    }

    public int NumDoneTasks() //Number of complite tasks..
    {
        return ArchiveTasks.Count;
    }

    public void SetColor(Color _color) //Set The Color of the Project.
    {
        ProjectColor = _color;
    }

    public void SetName(string _name) //Set The Name of the Project.
    {
        ProjectName = _name;
    }

    

	//// Use this for initialization
	//void Start () {
	//
	//}
	//
	//// Update is called once per frame
	//void Update () {
	//
	//}


}
