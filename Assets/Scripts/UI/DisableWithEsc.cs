using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWithEsc : MonoBehaviour {

    private static GameObject Panel;    

    private void OnEnable()
    {
        Panel = this.gameObject;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Cancel"))
        {               
            this.gameObject.SetActive(false);
        }
    }
    
    public static bool ActivePanel()
    {
        return Panel.activeSelf;
    }
}
