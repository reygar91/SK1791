﻿using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeMNGR : MonoBehaviour {

    public static TimeMNGR Instance;
    private Text TextComponent;
    public int timePassed = 1980; // day 1, 09:00 (in minutes); with speed =1 : 1 real time sec = 1 game time minute 
    public float timeSpeed = 1.0f;
    //public bool isPause = false;

    public Toggle Pause;

    private void Awake()
    {
        Instance = this;
        TextComponent = GetComponentInChildren<Text>();
        Pause.onValueChanged.AddListener(PauseUnpause); //Debug.Log("PauseListenerAdded");
    }

    // Use this for initialization
    void Start () {        
        StartCoroutine("PlayTime");
    }	

    private IEnumerator PlayTime()
    {
        while (true)
        {
            if (Pause.isOn)
            {
                yield return new WaitWhile(() => Pause.isOn);
            } else
            {
                int minutes = (timePassed % 60);
                int hours = (timePassed / 60) % 24;
                int days = (timePassed / 1440) % 365;

                string iHours;
                if (hours < 10)
                {
                    iHours = "0" + hours;
                }
                else iHours = hours.ToString();
                string iMinutes;
                if (minutes < 10)
                {
                    iMinutes = "0" + minutes;
                }
                else iMinutes = minutes.ToString();

                TextComponent.text = "Day: " + days + " Time: " + iHours + ":" + iMinutes;
                yield return new WaitForSeconds(1 / timeSpeed);
                timePassed++;
            }
        }
    }

    private void PauseUnpause(bool GamePaused)
    {
        if (GamePaused)
        {
            foreach (myCharacterController BC in CharacterMNGR.Instance.ActiveCharacters.ToArray())
            {
                BC.Stats.prevState = BC.Stats.state;
                BC.Stats.state = myCharacterStats.State.Pause;
                BC.AnimatorComponent.enabled = false;
            }
        }
        else
        {
            foreach (myCharacterController BC in CharacterMNGR.Instance.ActiveCharacters.ToArray())
            {
                BC.Stats.state = BC.Stats.prevState;
                BC.AnimatorComponent.enabled = true;
            }
        }
    }
}
