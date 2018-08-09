using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscMenu : MonoBehaviour {

    //public Button Save, Load;
    
    private void OnEnable()
    {
        Time.timeScale = 0;
        Debug.Log("Paused");
    }
   
    private void OnDisable()
    {
        Time.timeScale = 1;
        Debug.Log("Unpaused");
    }
   
}
