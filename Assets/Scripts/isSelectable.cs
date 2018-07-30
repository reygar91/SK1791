using System.Collections;
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
        CustPanel = MainSceneReferences.Instance.Windows.CustPanel.GetComponent<CustPanelScript>();
        PersPanel = MainSceneReferences.Instance.Windows.PersonelPanel.GetComponent<PersPanelScript>();
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
                    if (!CustPanel.gameObject.activeSelf)
                    {
                        CustPanel.gameObject.SetActive(true);
                    }
                    else if (previousObject == selectedObject)
                    {
                        CustPanel.gameObject.SetActive(false);
                    }
                    else
                    {
                        CustPanel.CustInitialize();
                    }
                    break;
                case "Personel":
                    if (!PersPanel.gameObject.activeSelf)
                    {
                        PersPanel.gameObject.SetActive(true);
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
