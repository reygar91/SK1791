using UnityEngine;
using System;

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
        if (BuildPosition_Y < 0)
            Permission = PermissionLower;
        else
            Permission = PermissionUpper;
    }

    public void SetGridValuesAroundNewRoom(Room newRoom)
    {
        SelectPermisionGrid(newRoom.saveData.Y);
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
        //appropriate room.name format is int(size)_string(name), ex. 6_Bar
        string[] stringSeparators = new string[] { "_" };
        string[] roomSizeAndName = room.name.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);       
        int roomSize = Convert.ToInt16(roomSizeAndName[0]); // 0 index cuz room size comes first
        int roomSizeKoef = roomSize / 3 - 1;
        return roomSizeKoef;
    }

}