using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeFlow : MonoBehaviour {

    private Text TextComponent;
    private int timePassed = 1980; // day 1, 09:00
    private float timeSpeed = 1.0f;

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

            TextComponent.text = "Day: " + days +" Time: " + iHours + ":" + iMinutes;
            yield return new WaitForSeconds(1 / timeSpeed);
            timePassed++;
        }
    }
}
