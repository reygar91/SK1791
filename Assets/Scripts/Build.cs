using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Build : MonoBehaviour {

    public Toggle[] Option;
    public GameObject[] RoomOriginals, InteriorsOriginals;
    /*Room Originals:
     * 0 - RoomSize 6
     * 1 - RoomSize 9
     * 2 - RoomSize 12
     */    
    public GameObject RoomContainer;
    private GameObject Room, Interior;

    private void Awake()
    {
        Room = RoomOriginals[0];
        Interior = InteriorsOriginals[0];
    }

    public void SelectRoom(int i)
    {
        if (Option[i].isOn)
        {
            Room.SetActive(false);
            Room = RoomOriginals[i];
            Room.SetActive(true);
            Option[i].isOn = false; // Disabling Toggle, so dont have to click twice when building similar constructions
        }
    }

    private void SelectBuildPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 p = ray.origin + (ray.direction * 10.0f); //Debug.Log("Position: " + p);
        int xCoor = 3 * Mathf.RoundToInt((p.x) / 3); // to make building spawn with step 3 on X
        int yCoor = 4 * Mathf.RoundToInt((p.y) / 4); // to make building spawn with step 4 on Y
        Room.transform.position = new Vector3(xCoor, yCoor - 0.1f, 0.9f);
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
        {
            GameObject newRoom = Instantiate(Room, RoomContainer.transform);
            newRoom.transform.position = new Vector3(xCoor, yCoor - 0.1f, 0.9f);
            Room.SetActive(false);
        }
    }

    public void SelectInterior(int i)
    {
        if (Option[i].isOn)
        {
            Interior.SetActive(false);
            Interior = InteriorsOriginals[i];
            Interior.SetActive(true);
            Option[i].isOn = false;
        }
    }

    public void SelectRoom()
    {

    }

	// Update is called once per frame
	void LateUpdate () {        
        if (Room.activeSelf == true)
        {
            SelectBuildPosition();
            if (Input.GetMouseButtonDown(1))
            {
                Room.SetActive(false);
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

}