using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWithEsc : MonoBehaviour {

    public static bool isActivePanels;    

    private void OnEnable()
    {
        isActivePanels = true;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Cancel"))
        {               
            this.gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        isActivePanels = false;
    }

    public static bool ActivePanel()
    {
        return isActivePanels;
    }
}
