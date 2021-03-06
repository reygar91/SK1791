﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMNGR : MonoBehaviour {

    public static BuildMNGR Instance;

    public Room reception;
    public Room[] Rooms;
    /*
     * 0 - Bar;
    */
    public List<Room> roomsList = new List<Room>();
    public GameObject RoomContainer;
    public BuildPreview Preview;
    private Room room;
    private BuildPermissionGrid Grid;



    public Toggle[] TypeToggles, SizeToggles;
    public RectTransform SizeGroup;
    private int RoomIndex;

    private void Awake()
    {
        Instance = this;
        Grid = new BuildPermissionGrid(4, 9);
    }

    public void SelectBuildPosition()
    {
        ////Defining Rules Of Movement in the World
        Vector3 newPosition = UI_helper.MoveWithMouse(3, new Vector3(0, 0, 0));//3 because 1 room section is 3 units long
        if (newPosition.x < 0)
        {
            newPosition.x = 0;
        }
        if (Grid.AllowedToBuild(newPosition, room))
        {
            Preview.transform.position = newPosition;
            if (Input.GetMouseButtonDown(0) && !UI_helper.isPointerOverUI2())
            {
                Room newRoom = InstantiateRoom(room, newPosition);
                GoldMNGR.Instance.SubtractGold(newRoom.BuildPrice);
                newRoom.boxCollider.gameObject.layer = 0;
                ResetPreview();
            }
        }
        else
            Preview.transform.position = new Vector3(-12, 4, 0);
    }

    public Room InstantiateRoom(Room room, Vector3 position)
    {
        Room newRoom = Instantiate(room, RoomContainer.transform);
        newRoom.transform.position = position;
        newRoom.gameObject.SetActive(true); // without this rooms will be inactive after Loading game
        Grid.SetGridValuesAroundNewRoom(newRoom);
        roomsList.Add(newRoom);
        newRoom.CustomAwake();
        return newRoom;
    }

    public void ResetPreview()
    {
        if (room)
        {
            Preview.transform.position = new Vector3(-12, 4, 0);
            room.transform.SetParent(Preview.transform.parent);
            room.gameObject.SetActive(false);
            Preview.gameObject.SetActive(false);
        }        
    }


    public void SelectSize(int i)// assingned in inspector
    {
        if (SizeToggles[i].isOn)
        {
            room = Rooms[3 * RoomIndex + i];
            room.transform.SetParent(Preview.transform);
            room.gameObject.SetActive(true);
            Preview.gameObject.SetActive(true);
            SizeToggles[i].isOn = false; // Disabling Toggle, so dont have to click twice when building similar constructions
            //TypeToggles[RoomIndex].isOn = false;
            SizeGroup.gameObject.SetActive(false);

            room.typeAndSizeID = 3 * RoomIndex + i;
        }
    }

    public void SelectType(int i) // assingned in inspector
    {
        if (TypeToggles[i].isOn)
        {
            RoomIndex = i;
            CheckBuildPrices("");
            SizeGroup.gameObject.SetActive(true);
            TypeToggles[i].isOn = false;
        }
    }

    public void CheckBuildPrices(string input)
    {
        int Gold = GoldMNGR.Instance.Gold;
        for (int i = 0; i < SizeToggles.Length; i++)
        {           
            SizeToggles[i].interactable = false;
            if (Rooms[3 * RoomIndex + i].BuildPrice > Gold)
            {
                SizeToggles[i].interactable = false;
            }                
            else SizeToggles[i].interactable = true;
        }
    }


}
