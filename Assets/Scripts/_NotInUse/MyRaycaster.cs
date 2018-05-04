using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyRaycaster : MonoBehaviour {

    private GameController Controller;
    private GameObject selectedObject;

    private void Awake()
    {
        Controller = GetComponent<GameController>();
    }
    private void Update()
    {
        RaycastHit();
    }

    private void RaycastHit()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse is down");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Physics.Raycast()
            RaycastHit2D hit2D = Physics2D.Raycast(ray.origin, ray.direction);
            //Debug.Log("rayOrigin " + ray.origin + "; rayDirection " + ray.direction);
            //Debug.DrawRay(ray.origin, ray.direction);
            //Debug.DrawLine(ray.origin, ray.direction);
            if (hit2D)
            {
                if (!IsPointerOverUIObject())
                {
                    selectedObject = hit2D.transform.gameObject;
                    Debug.Log("Hit " + selectedObject.name);
                    if (selectedObject.tag == "Customer")
                    {
                        Controller.Panels[0].SetActive(true);
                    }
                    if (selectedObject.tag == "Personel")
                    {
                        Controller.Panels[1].SetActive(true);
                    }
                }
            }
            else
            {
                Debug.Log("No hit");
            }
        }
    }

    //When Touching UI check
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public GameObject getSelectedObject()
    {
        return selectedObject;
    }
}
