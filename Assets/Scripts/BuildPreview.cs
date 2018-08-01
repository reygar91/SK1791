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
    //private int roomSizeKoef;

    //private int[,] PermissionUpper, PermissionLower;

    private BuildPermissionGrid Grid;

    private void Awake()
    {
        //InitializePermissionGrids();
        Grid = new BuildPermissionGrid(4, 9);
    }

    //private void InitializePermissionGrids()
    //{
    //    int GridXLenght = 9;
    //    int GridYHeight = 4;
    //    PermissionUpper = new int[GridYHeight, GridXLenght];
    //    PermissionLower = new int[GridYHeight, GridXLenght];
    //    //assinning values 0 - strict not allowed, 2 - allowed; 1 soft not allowed
    //    for (int row = 0; row < GridYHeight; row++)
    //    {
    //        for (int col = 0; col < GridXLenght; col++)
    //        {
    //            if (row == 0 && col == 3)
    //            {
    //                PermissionUpper[row, col] = 2;
    //            }
    //            else if (row == 1 && col < 3)
    //            {
    //                PermissionUpper[row, col] = 2;
    //                PermissionUpper[row - 1, col] = 0;
    //                PermissionLower[row, col] = 2;
    //            }
    //            else
    //            {
    //                PermissionUpper[row, col] = 1;
    //                PermissionLower[row, col] = 1;
    //            }
    //        }
    //    }
    //}

    private void OnEnable()
    {
        child = GetComponentInChildren<Room>();
        //roomSizeKoef = Grid.GetRoomSizeKoef(child);        
    }
   
    private void SelectBuildPosition()
    {
        ////Defining Rules Of Movement in the World
        Vector3 newPosition = UI_helper.MoveWithMouse(3, new Vector3(0, 0, 0));//3 because 1 room section is 3 units long
        if (newPosition.x < 0)
        {
            newPosition.x = 0;
        }
        if (Grid.AllowedToBuild(newPosition, child))
        {
            gameObject.transform.position = newPosition;
            if (Input.GetMouseButtonDown(0) && !UI_helper.isPointerOverUI2())
            {
                Room newRoom = InstantiateRoom(child, newPosition);
                Grid.SetGridValuesAroundNewRoom(newRoom);
                ResetPreview();
            }
        } else
            transform.position = new Vector3(-12, 4, 0);
        //int GridX = Mathf.RoundToInt(newPosition.x / 3); 
        //int GridY = Mathf.RoundToInt(newPosition.y / 4);
        ////Selecting Upper or Lower permission grid
        //int[,] Permission;
        //if (GridY < 0)
        //{
        //    Permission = PermissionLower;
        //} else
        //{
        //    Permission = PermissionUpper;
        //}
        //int GridYAbs = Mathf.Abs(GridY); //
        /////
        //if (Permission[GridYAbs, GridX] != 0 && (GridX + roomSizeKoef) < Permission.Length/4) //4 is GridYHeight
        //{
        //    ////Checking inf build allowed here
        //    int BuildAllowed = 1;
        //    for (int col = GridX + roomSizeKoef; col >= GridX; col--)
        //    {
        //        BuildAllowed = BuildAllowed * Permission[GridYAbs, col];
        //    }
        //    ////
        //    if (BuildAllowed > 1)
        //    {
        //        gameObject.transform.position = newPosition;
        //        if (Input.GetMouseButtonDown(0) && !UI_helper.isPointerOverUI2())
        //        {
        //            InstantiateRoom(child, newPosition);

        //            ResetPreview();
        //            //add new room to roomlist was here
        //            //setting allowed to build spots in array

        //            //for (int col = GridX + roomSizeKoef; col >= GridX; col--)
        //            //{
        //            //    Permission[GridYAbs, col] = 0;//deny building where new room is
        //            //    Permission[GridYAbs + 1, col] = 2;//allow building over(under) new room
        //            //    if (GridY == 0)
        //            //        PermissionLower[1, col] = 2;//on Y=0 Upper is selected, so need to set Lower permissions manually
        //            //}
        //            ////if there is a cell after new room, setting allowed (number 2) to it
        //            //if (GridY == 0 && (GridX + roomSizeKoef + 1) < Permission.Length / 4)
        //            //    Permission[0, GridX + roomSizeKoef + 1] = 2;
        //            //end
        //        }
        //    }     
        //}
        //else
        //{
        //    transform.position = new Vector3(-12, 4, 0);
        //}
    }

    


    void LateUpdate()
    {
        SelectBuildPosition();
        if (Input.GetMouseButtonDown(1))
        {
            ResetPreview();
        }
    }

    private void ResetPreview()
    {
        transform.position = new Vector3(-12,4,0);
        child.transform.SetParent(transform.parent);     
        child.gameObject.SetActive(false);        
        gameObject.SetActive(false);
    }

    public Room InstantiateRoom(Room room, Vector3 position)
    {
        Room newRoom = Instantiate(room, RoomContainer.transform);
        newRoom.transform.position = position;
        newRoom.SaveRoomPosition();
        BuildMNGR.Instance.roomsList.Add(newRoom);
        return newRoom;
    }

}

public class BuildPermissionGrid
{
    private int[,] PermissionUpper, PermissionLower, Permission;

    public BuildPermissionGrid(int GridYHeight, int GridXLenght)
    {
        PermissionUpper = new int[GridYHeight, GridXLenght];
        PermissionLower = new int[GridYHeight, GridXLenght];
        SetGridDefaultValues(GridYHeight, GridXLenght);
    }

    private void SetGridDefaultValues(int GridYHeight, int GridXLenght)
    {
        //assinning values:
        //0 - strict not allowed, 2 - allowed; 1 soft not allowed
        for (int row = 0; row < GridYHeight; row++)
        {
            for (int col = 0; col < GridXLenght; col++)
            {
                if (row == 0 && col == 3)
                {
                    PermissionUpper[row, col] = 2;
                }
                else if (row == 1 && col < 3)
                {
                    PermissionUpper[row, col] = 2;
                    PermissionUpper[row - 1, col] = 0;
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

    public bool AllowedToBuild(Vector3 BuildPosition, Room room)
    {
        SelectPermisionGrid(BuildPosition.y);
        int X = Mathf.RoundToInt(BuildPosition.x / 3);
        int Y = Mathf.RoundToInt(BuildPosition.y / 4);
        Y = Mathf.Abs(Y);
        int roomSizeKoef = GetRoomSizeKoef(room);
        if (NotExceedGridLenght(X, roomSizeKoef) && NotForbidden(X, Y, roomSizeKoef))
            return true;
        else return false;
    }

    private void SelectPermisionGrid(float BuildPosition_Y)
    {        
        if(BuildPosition_Y < 0)
            Permission = PermissionLower;
        else
            Permission = PermissionUpper;
    }

    public void SetGridValuesAroundNewRoom(Room newRoom)
    {
        int X = Mathf.RoundToInt(newRoom.saveData.X / 3);
        int Y = Mathf.RoundToInt(newRoom.saveData.Y / 4);
        Y = Mathf.Abs(Y); //
        int roomSizeKoef = GetRoomSizeKoef(newRoom);
        for (int col = X + roomSizeKoef; col >= X; col--)
        {
            Permission[Y, col] = 0;//deny building where new room is
            Permission[Y + 1, col] = 2;//allow building over(under) new room
            if (Y == 0)
                PermissionLower[1, col] = 2;//on Y=0 Upper is selected, so need to set Lower permissions manually
        }
        //if there is a cell after new room, setting allowed (number 2) to it
        if (Y == 0 && (X + roomSizeKoef + 1) < Permission.Length / 4)
            Permission[0, X + roomSizeKoef + 1] = 2;
    }

    private bool NotExceedGridLenght(int X, int roomSizeKoef)
    {
        return ((X + roomSizeKoef) < Permission.Length / 4);
    }

    private bool NotForbidden(int X, int Y, int roomSizeKoef)
    {
        int BuildAllowed = 1;
        for (int col = X + roomSizeKoef; col >= X; col--)
        {
            BuildAllowed = BuildAllowed * Permission[Y, col];
        }
        if (BuildAllowed > 1)
            return true;
        else return false;
    }

    public int GetRoomSizeKoef(Room room)
    {
        int startIndex = room.name.IndexOf("_");
        string Size = room.name.Substring(startIndex + 1); //Debug.Log("SizeString="+ Size);
        int roomSize = Convert.ToInt16(Size);
        int roomSizeKoef = roomSize / 3 - 1;
        return roomSizeKoef;
    }


}
