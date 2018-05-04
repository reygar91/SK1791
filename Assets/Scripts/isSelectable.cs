using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class isSelectable : MonoBehaviour {

    private static GameController Controller;
    private static CustPanelScript CustPanel;
    private static PersPanelScript PersPanel;
    public static GameObject selectedObject;
    public static GameObject previousObject;

    private void Awake()
    {
        Controller = FindObjectOfType<GameController>(); //Debug.Log(Controller);
        CustPanel = Controller.Panels[0].GetComponent<CustPanelScript>(); //Debug.Log(CustPanel);
        PersPanel = Controller.Panels[1].GetComponent<PersPanelScript>(); //Debug.Log(PersPanel);
    }

    private void OnMouseDown()
    {
        if (!IsPointerOverUIObject())
        {
            previousObject = selectedObject; //if (previousObject) Debug.Log("previous " + previousObject.name);
            selectedObject = gameObject; //Debug.Log("selected " + selectedObject.name);
            switch (tag)
            {
                case "Customer":
                    if (!Controller.Panels[0].activeSelf)
                    {
                        Controller.Panels[0].SetActive(true);
                    }
                    else if (previousObject == selectedObject)
                    {
                        Controller.Panels[0].SetActive(false);
                    }
                    else
                    {
                        CustPanel.CustInitialize();
                    }
                    break;
                case "Personel":
                    if (!Controller.Panels[1].activeSelf)
                    {
                        Controller.Panels[1].SetActive(true);
                    }
                    else
                    {
                        PersPanel.Initialize();
                    }
                    break;
            }
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}
