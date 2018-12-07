using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseUpActivitySelector : MonoBehaviour {

    [SerializeField] Button[] buttons;
    Text[] Description;
    private Activity[] activities;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>(true);
        Description = new Text[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            Description[i] = buttons[i].GetComponentInChildren<Text>();
            buttons[i].onClick.AddListener(Disable);
        }
    }

    public void FormLisOfActivities()
    {
        activities = Room.selectedRoom.Activities.ToArray();
        for (int i = 0; i < activities.Length; i++)
        {
            buttons[i].gameObject.SetActive(true);            
            buttons[i].onClick.AddListener(activities[i].SetActivityToSelectedPersonnel);
            Description[i].text = activities[i].GetType().ToString();
        }
    }

    private void OnEnable()
    {
        InputMNGR.Instance.Pause();
        FormLisOfActivities();
    }
    private void OnDisable()
    {
        for (int i = 0; i < activities.Length; i++)
        {
            buttons[i].onClick.RemoveListener(activities[i].SetActivityToSelectedPersonnel);
            buttons[i].gameObject.SetActive(false);
        }
        InputMNGR.Instance.Pause();
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
