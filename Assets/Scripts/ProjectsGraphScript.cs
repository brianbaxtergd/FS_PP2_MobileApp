using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProjectsGraphScript : MonoBehaviour
{
    // Components.
    public GameObject applicationController;
    //public Image wedgePrefab;
    public GameObject wedgePrefab;

    // Data.
    ArrayList wedges; // Holds instances of wedgePrefabs.

	// Initialize projects graph.
	void Start ()
    {
        wedges = new ArrayList();
	}
	
    // Update called each frame.
	void Update ()
    {
	}

    // Destroys & rebuilds the projects graph to match DataController's ArrayList of projects.
    public void UpdateGraph()
    {
        /// Destroy existing wedges. ///
        DestroyGraph();

        /// Rebuild wedges from Project data. ///
        // Store script in data controller.
        DataController dc = applicationController.GetComponent<DataController>();
        // Initialize all wedges.
        for (int i = 0; i < dc.GetProjectCount(); i++)
        {
            Project p = dc.mProjectList[i] as Project;
            GameObject w = Instantiate(wedgePrefab);
            w.GetComponent<WedgeScript>().InitWedgePrefab(
                i,
                CalculateWedgeAngle(i),
                CalculateWedgeRadius(i),
                p.GetColor());
            wedges.Add(w);
        }
        // Apply data to wedge components.
        float zRot = 0.0f;
        foreach (GameObject item in wedges)
        {
            item.transform.SetParent(transform, false); // Todo: What does this do?
            WedgeScript ws = item.GetComponent<WedgeScript>();
            // Update image angle.
            item.GetComponent<Image>().fillAmount = ws.angle;
            item.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, zRot));
            zRot += item.GetComponent<Image>().fillAmount * 360;
            // Update image radius.
            float scale = ws.radius; //(ws.radius / 2.0f) + 0.5f;
            if (scale < 0.1)
                scale = 0.1f;
            else if (scale > 0.9)
                scale = 0.9f;
            item.transform.localScale = new Vector3(scale, scale, scale);
            // Update image color.
            item.GetComponent<Image>().color = ws.color;
        }
    }

    public void DestroyGraph()
    {
        foreach (GameObject item in wedges)
        {
            Destroy(item);
        }
        wedges.Clear();
    }

    public float CalculateWedgeAngle(int _index)
    {
        DataController dc = applicationController.GetComponent<DataController>();
        Project p = dc.mProjectList[_index] as Project;
        int projectTaskCount = p.GetTotalTaskCount();
        int totalTaskCount = dc.GetTotalTaskCountInAllProjects();
        float a = (float)projectTaskCount / (float)totalTaskCount;
        return a;
    }

    public float CalculateWedgeRadius(int _index)
    {
        DataController dc = applicationController.GetComponent<DataController>();
        Project p = dc.mProjectList[_index] as Project;
        int completedTaskCount = p.GetArchivedTaskCount();
        float r = 1.0f - ((float)completedTaskCount / (float)p.GetTotalTaskCount());
        return r;
    }
}
