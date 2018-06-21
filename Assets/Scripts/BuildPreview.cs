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
    private int roomSizeKoef;

    private int[,] PermissionUpper, PermissionLower;

    private void Awake()
    {
        int GridXLenght = 9;
        int GridYHeight = 4;
        PermissionUpper = new int[GridYHeight, GridXLenght];
        PermissionLower = new int[GridYHeight, GridXLenght];
        //assinning values 0 - strict not allowed, 2 - allowed; 1 soft not allowed
        for (int row = 0; row < GridYHeight; row++)
        {
            for (int col = 0; col < GridXLenght; col++)
            {
                if (row == 0 && col == 3)
                {
                    PermissionUpper[row, col] = 2;
                } else if (row == 1 && col < 3)
                {
                    PermissionUpper[row, col] = 2;
                    PermissionUpper[row-1, col] = 0;
                    PermissionLower[row, col] = 2;
                }
                else
                {
                    PermissionUpper[row, col] = 1;
                    PermissionLower[row, col] = 1;
                }               
            }
        }
    }
    private void OnEnable()
    {
        //child = gameObject.transform.GetChild(0).gameObject;
        child = GetComponentInChildren<Room>();
        int startIndex = child.name.IndexOf("_");
        string Size = child.name.Substring(startIndex + 1); //Debug.Log("SizeString="+ Size);
        int roomSize = Convert.ToInt16(Size);
        roomSizeKoef = roomSize / 3 - 1;
    }
   
    private void SelectBuildPosition()
    {
        Vector3 newPosition = UI_helper.MoveWithMouse(3, new Vector3(0, 0, 0));
        if (newPosition.x < 0)
        {
            newPosition.x = 0;
        }
        int GridX = Mathf.RoundToInt(newPosition.x / 3); //Debug.Log(GridX);
        int GridY = Mathf.RoundToInt(newPosition.y / 4);
        int[,] Permission;
        if (GridY < 0)
        {
            Permission = PermissionLower;
        } else
        {
            Permission = PermissionUpper;
        }
        int GridYAbs = Mathf.Abs(GridY); //Debug.Log("length" +Permission.Length + "gridX+"+ GridX + roomSizeKoef);
        if (Permission[GridYAbs, GridX] != 0 && (GridX + roomSizeKoef) < Permission.Length/4)
        {
            int BuildAllowed = 1;
            for (int col = GridX + roomSizeKoef; col >= GridX; col--)
            {
                BuildAllowed = BuildAllowed * Permission[GridYAbs, col];
            }
            if (BuildAllowed > 1)
            {
                gameObject.transform.position = newPosition;
                if (Input.GetMouseButtonDown(0) && !UI_helper.isPointerOverUI2())
                {
                    Room newRoom = Instantiate(child, RoomContainer.transform);
                    newRoom.transform.position = newPosition;
                    resetPreview();
                    Room.roomsList.Add(newRoom);
                    //setting allowed to build spots in array
                    if (GridY == 0)
                    {
                        if ((GridX + roomSizeKoef + 1) < Permission.Length / 4)
                        {
                            Permission[0, GridX + roomSizeKoef + 1] = 2;
                        }                        
                        for (int col = GridX + roomSizeKoef; col >= GridX; col--)
                        {
                            Permission[0, col] = 0;
                            Permission[1, col] = 2;
                            PermissionLower[1, col] = 2;
                        }
                    } else
                    {
                        for (int col = GridX + roomSizeKoef; col >= GridX; col--)
                        {
                            Permission[GridYAbs, col] = 0;
                            Permission[GridYAbs + 1, col] = 2;
                        }
                    }
                    //end
                }
            }     
        }
        else
        {
            transform.position = new Vector3(-12, 4, 0);
        }
    }

    void LateUpdate()
    {
        SelectBuildPosition();
        if (Input.GetMouseButtonDown(1))
        {
            resetPreview();
        }
    }

    private void resetPreview()
    {
        transform.position = new Vector3(-12,4,0);
        child.transform.SetParent(transform.parent);     
        child.gameObject.SetActive(false);        
        gameObject.SetActive(false);
    }


}
