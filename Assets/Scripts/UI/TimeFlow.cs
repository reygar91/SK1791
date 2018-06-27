﻿using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeFlow : MonoBehaviour {

    private Text TextComponent;
    private int timePassed = 1980; // day 1, 09:00
    public static float timeSpeed = 1.0f;
    public static bool isPause = false;

    private void Awake()
    {
        TextComponent = GetComponent<Text>();
    }

    // Use this for initialization
    void Start () {
        StartCoroutine("PlayTime");
    }	

    private IEnumerator PlayTime()
    {
        while (true)
        {
            if (isPause)
            {
                yield return new WaitWhile(() => isPause);
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

    public void Pause()
    {
        
        /*
        isPause = !isPause;
        foreach (Character item in GameController.CharList)
        {
            if (item.gameObject.activeSelf)
            {
                item.AnimatorComponent.enabled = !item.AnimatorComponent.enabled;
            }
        }
        */
    }
}
