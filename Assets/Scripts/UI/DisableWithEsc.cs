using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWithEsc : MonoBehaviour {

    private static GameObject Panel;    

    private void OnEnable()
    {
        Panel = gameObject;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Cancel"))
        {               
            gameObject.SetActive(false);
        }
    }
    
    public static bool ActivePanel()
    {
        if (Panel)
        {
            return Panel.activeSelf;
        }
        else return false;        
    }
}
