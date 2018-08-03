using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildPreview : MonoBehaviour {

    public Room[] Rooms;
    /*
     * 0 - Bar;
    */
    public GameObject RoomContainer;
    private Room child;   

    private void OnEnable()
    {
        child = GetComponentInChildren<Room>();       
    }

    void LateUpdate()
    {
        SelectBuildPosition();
        if (Input.GetMouseButtonDown(1))
        {
            ResetPreview();
        }
    }

    private void SelectBuildPosition()
    {
        ////Defining Rules Of Movement in the World
        Vector3 newPosition = UI_helper.MoveWithMouse(3, new Vector3(0, 0, 0));//3 because 1 room section is 3 units long
        if (newPosition.x < 0)
        {
            newPosition.x = 0;
        }
        if (BuildMNGR.Instance.Grid.AllowedToBuild(newPosition, child))
        {
            gameObject.transform.position = newPosition;
            if (Input.GetMouseButtonDown(0) && !UI_helper.isPointerOverUI2())
            {
                InstantiateRoom(child, newPosition);                
                ResetPreview();
            }
        } else
            transform.position = new Vector3(-12, 4, 0);
    }

    public Room InstantiateRoom(Room room, Vector3 position)
    {
        Room newRoom = Instantiate(room, RoomContainer.transform);
        newRoom.transform.position = position;
        newRoom.gameObject.SetActive(true);
        newRoom.SaveRoomPosition();
        BuildMNGR.Instance.Grid.SetGridValuesAroundNewRoom(newRoom);
        BuildMNGR.Instance.roomsList.Add(newRoom);
        return newRoom;
    }

    private void ResetPreview()
    {
        transform.position = new Vector3(-12,4,0);
        child.transform.SetParent(transform.parent);     
        child.gameObject.SetActive(false);        
        gameObject.SetActive(false);
    }    

}


