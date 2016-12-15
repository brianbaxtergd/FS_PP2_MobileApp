using UnityEngine;
using UnityEngine.UI;
//using System.Collections;

public class ApplicationStateMachineScript : MonoBehaviour
{
    // State-machine data.
    public enum appStates
    {
        home = 0,
        project,
        quickTimer
    }
    private appStates state;

    // Components.
    public GameObject panelHome;
    public GameObject panelProject;
    public GameObject panelQuickTimer;
    public Text toolBarTitleText;
    public GameObject applicationController;
    public GameObject panelSettings;

    // User settings.
    bool timerAudio = true;
    bool timerMessages = true;

    // Unity methods.
    void Start()
    {
        state = appStates.home;
        toolBarTitleText.text = "Home";
    }
    void Update()
    {

    }

    // Accessors.
    public appStates GetState()
    {
        return state;
    }
    public bool GetTimerAudio()
    {
        return timerAudio;
    }
    public bool GetTimerMessages()
    {
        return timerMessages;
    }

    // Mutators.
    public void SetState(appStates _newState)
    {
        if (_newState == state)
            return;
        else
        {
            // Store previous state.
            appStates prevState = state;
            // Update state.
            state = _newState;
            // Initialize new state.
            switch (state)
            {
                case appStates.home:
                    panelHome.SetActive(true);
                    toolBarTitleText.text = "Home";
                    break;
                case appStates.project:
                    panelProject.SetActive(true);
                    // Update tool bar title.
                    DataController dc = applicationController.GetComponent<DataController>();
                    Project p = dc.mProjectList[dc.CurrentProjectIndex] as Project;
                    toolBarTitleText.text = p.Name;
                    // Update canvas bg color.
                    panelProject.GetComponent<Image>().color = p.GetColor();
                    break;
                case appStates.quickTimer:
                    panelQuickTimer.SetActive(true);
                    // Update tool bar title.
                    toolBarTitleText.text = "Quick Timer";
                    break;
                default:
                    break;
            }
            // Close old state.
            CloseState(prevState);
        }
    }

    // Misc. methods.
    private void CloseState(appStates _prevState)
    {
        switch (_prevState)
        {
            case appStates.home:
                panelHome.SetActive(false);
                break;
            case appStates.project:
                panelProject.SetActive(false);
                break;
            case appStates.quickTimer:
                panelQuickTimer.SetActive(false);
                break;
            default:
                break;
        }
    }
    public void OnClick_QuickTimerButton()
    {
        // Switch to QuickTimer-state.
        SetState(appStates.quickTimer);
    }
    public void OnClick_HomeButton()
    {
        // Switch to Home-state.
        SetState(appStates.home);
    }
    public void OnClick_ProjectButton()
    {
        // Switch to Project-state.
        SetState(appStates.project);
    }
    public void onClick_ToggleSettings()
    {
        if (!panelSettings.activeSelf)
        {
            // Update toolbar title text.
            toolBarTitleText.text = "Settings";
            // Open settings.
            panelSettings.SetActive(true);
        }
        else
        {
            // Update all values.
            timerAudio = panelSettings.transform.FindChild("TimerAudioToggle").GetComponent<Toggle>().isOn;
            timerMessages = panelSettings.transform.FindChild("TimerMessagesToggle").GetComponent<Toggle>().isOn;
            // Close settings.
            panelSettings.SetActive(false);
            // Revert toolbar title text.
            switch(state)
            {
                case appStates.home:
                    toolBarTitleText.text = "Home";
                    break;
                case appStates.project:
                    DataController dc = applicationController.GetComponent<DataController>();
                    Project p = dc.mProjectList[dc.CurrentProjectIndex] as Project;
                    toolBarTitleText.text = p.Name;
                    break;
                case appStates.quickTimer:
                    toolBarTitleText.text = "Quick Timer";
                    break;
                default:
                    break;
            }
        }

    }
}
