﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isSelectable : MonoBehaviour {

    //private static GameController Controller;
    private static CustPanelScript CustPanel;
    private static PersPanelScript PersPanel;
    public static GameObject selectedObject;
    public static GameObject previousObject;

    private void Awake()
    {
        //Controller = GameController.Instance;
        CustPanel = GameMNGR.Instance.Panels[0].GetComponent<CustPanelScript>(); //Debug.Log(CustPanel);
        PersPanel = GameMNGR.Instance.Panels[1].GetComponent<PersPanelScript>(); //Debug.Log(PersPanel);
    }

    private void OnMouseOver() //OnMouseDown() could be used for left click, but for right click OnMouseOver + Input.GetMouseButtonDown(1)
    {
        if (Input.GetMouseButtonDown(1) && !UI_helper.isPointerOverUI())
        {
            previousObject = selectedObject; //if (previousObject) Debug.Log("previous " + previousObject.name);
            selectedObject = gameObject; //Debug.Log("selected " + selectedObject.name);
            switch (tag)
            {
                case "Customer":
                    if (!GameMNGR.Instance.Panels[0].activeSelf)
                    {
                        GameMNGR.Instance.Panels[0].SetActive(true);
                    }
                    else if (previousObject == selectedObject)
                    {
                        GameMNGR.Instance.Panels[0].SetActive(false);
                    }
                    else
                    {
                        CustPanel.CustInitialize();
                    }
                    break;
                case "Personel":
                    if (!GameMNGR.Instance.Panels[1].activeSelf)
                    {
                        GameMNGR.Instance.Panels[1].SetActive(true);
                    }
                    else
                    {
                        PersPanel.Initialize();
                    }
                    break;
            }
        }
    }
}
