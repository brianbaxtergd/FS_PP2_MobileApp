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

    // Unity methods.
    void Start()
    {
        state = appStates.home;
    }
    void Update()
    {

    }

    // Accessors.
    public appStates GetState()
    {
        return state;
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
                    break;
                case appStates.project:
                    panelProject.SetActive(true);
                    break;
                case appStates.quickTimer:
                    panelQuickTimer.SetActive(true);
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
        // Determine which wedge was clicked & set 'curProject' value in application controller?
        // Switch to Project-state.
        SetState(appStates.project);
    }
}
