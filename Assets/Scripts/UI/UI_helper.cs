using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_helper : MonoBehaviour {

    public static bool isPointerOverUI()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public static Vector3 MoveWithMouse(GameObject item,int xStep, Vector3 offset)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 p = ray.origin + (ray.direction * 10.0f); //Debug.Log("Position: " + p);
        int xCoor = xStep * Mathf.RoundToInt((p.x) / xStep); // to make building spawn with xStep on X
        int yCoor = 4 * Mathf.RoundToInt((p.y) / 4); // to make building spawn with step 4 on Y
        item.transform.position = new Vector3(xCoor + offset.x, yCoor + offset.y, offset.z);
        return item.transform.position;
    }

}
