﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TaskController : MonoBehaviour {
    Text textName;
    Button buttonCheck;
	// Use this for initialization
	void Start () {
        textName = GetComponentInChildren<Text>();
        buttonCheck = GetComponentInChildren<Button>();
    }
	// Update is called once per frame
	void Update () {
	}
    public void SetName (string name)
    {
        if (textName != null) // Null-check resolves a warning/error Unity has been throwing. BB
            textName.text = name;
    }
    public void Activate (bool active)
    {
        enabled = active;
    }
}
