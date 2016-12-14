using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;
using System;

public class Wedge : MonoBehaviour
{
    public int importance;
    public float completion;
    public Color color;
    public Image image;
    public Wedge() {}
    public Wedge(Image _prefab, float _completion, Color _color, int _importance)
    {
        image = _prefab;
        importance = _importance;
        completion = _completion;
        color = _color;
    }
    public Wedge(float _completion, Color _color, int _importance)
    {
        importance = _importance;
        completion = _completion;
        color = _color;
    }
    public void Init(Image _prefab, float _completion, Color _color, int _importance)
    {
        image = _prefab;
        importance = _importance;
        completion = _completion;
        color = _color;
    }
    public void Init(float _completion, Color _color, int _importance)
    {
        importance = _importance;
        completion = _completion;
        color = _color;
    }
}
public class PieGraph : MonoBehaviour
{
    ArrayList wedges = new ArrayList();
    public Image wedgePrefab;
    float total;
    DataController mDataController;
	// Use this for initialization
	void Start ()
    {
        mDataController = GetComponent<DataController>();
        DrawGraph();
	}
    public void DrawGraph()
    {
        float zRotation = 0.0f;
        total = 0;
        foreach (Wedge item in wedges)
        {
            total += item.importance;
        }
        foreach (Wedge item in wedges)
        {
            item.image.transform.SetParent(transform, false);
            item.image.color = item.color;
            item.image.fillAmount = item.importance / total;
            item.image.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, zRotation));
            float scale = (item.completion / 2.0f) + 0.5f;
            item.image.transform.localScale = new Vector3(scale, scale, scale);
            zRotation += item.image.fillAmount * 360;
        }
    }
    public void Add(float _completion, Color _color, int _importance)
    {
        wedges.Add(Instantiate(wedgePrefab));
        Wedge newWedge = new Wedge(Instantiate(wedgePrefab) as Image, _completion, _color, _importance);
        //Wedge newWedge = gameObject.AddComponent<Wedge>();
        //newWedge.Init(Instantiate(wedgePrefab) as Image, _completion, _color, _importance);
        wedges.Add(newWedge);
    }
    //Update is called once per minute or everey change
    void Update ()
    {
        DrawGraph();
	}
    public void Test()
    {
        //wedges.Clear();
        System.Random rng = new System.Random();
        Color color = Color.yellow;
        bool cont = true;
        while (cont)
        {
            color = new Color(rng.Next(255) * 0.00392f, rng.Next(255) * 0.00392f, rng.Next(255) * 0.00392f);
            if (color != Color.white)
            {
                cont = false;
                break;
            }
            foreach (Wedge item in wedges)
            {
                if (color != item.color)
                {
                    cont = false;
                    break;
                }
            }
        }
        Add(rng.Next(1, 100) * 0.01f, color, rng.Next(1, 10));
    }
    public void DestroyGraph()
    {
        foreach (Wedge item in wedges)
        {
            Destroy(item.image.gameObject);
        }
        wedges.Clear();
    }
}
