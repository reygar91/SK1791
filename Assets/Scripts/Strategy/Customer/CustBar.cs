using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustBar : ICustBehaviour
{
    Customer cust;
    Bar bar;
    Animator animator;
    GameObject Seat;
    Vector3 targetVector;
    int StatusID;

    //int counter = 0;

    public CustBar(Customer customer, Bar RoomType)
    {
        cust = customer;
        bar = RoomType;
        StatusID = 0;
    }   

    public Vector3 LeaveRoom()
    {
        switch (StatusID)
        {
            case 101:
                targetVector = new Vector3(bar.Doors.transform.position.x, bar.Doors.transform.position.y, bar.MiddleOfTheRoom.transform.position.z);
                StatusID = 102;
                break;
            case 102:
                targetVector = bar.Doors.transform.position;
                StatusID = 103;
                break;
            case 103:
                cust.transform.position = cust.reception.InternalDoor.transform.position;
                targetVector = new Vector3(cust.transform.position.x, cust.transform.position.y, cust.reception.EntrancePoint.transform.position.z);
                StatusID = 104;
                break;
            case 104:
                targetVector = new Vector3(cust.transform.position.x, cust.transform.position.y, cust.reception.EntrancePoint.transform.position.z);
                break;
            default://move to the center of the room
                targetVector = new Vector3(cust.transform.position.x, cust.transform.position.y, bar.MiddleOfTheRoom.transform.position.z);
                StatusID = 101;
                break;
        }
        return targetVector;
    }

    public Vector3 RoomBehaviour()
    {
        switch (StatusID)
        {
            case 1: //find a seat and select it as new target
                if (bar.Seats.Count != 0 && !Seat)
                {
                    int RandomSeat = Random.Range(0, bar.Seats.Count);
                    Seat = bar.Seats[RandomSeat];
                    bar.Seats.Remove(Seat);
                    targetVector = Seat.transform.position;
                    StatusID = 2;
                }
                break;
            case 2: //adding cust to custAtBar list + setting his orientation to correct side
                    bar.custAtBar.Add(cust);
                    int startIndex = Seat.name.IndexOf("_");
                    string orientation = Seat.name.Substring(startIndex + 1);
                    switch (orientation)
                    {
                        case "Right":
                            cust.transform.rotation = new Quaternion(0, -1, 0, 0);//facing from right to left
                            break;
                        case "Left":
                            cust.transform.rotation = new Quaternion(0, 0, 0, 1);//facing from left to right
                            break;
                    }
                    StatusID = 3;
                break;
            case 3://here we play animation for cust.AnimationTime duration
                cust.AnimationTime = 5f; //Debug.Log(StatusID);
                cust.Wait = true;                
                StatusID = 4;
                break;
            case 4://after main action finished playing idle animation till the rest of patience
                cust.AnimationTime = cust.Patience; //Debug.Log(StatusID);
                cust.Wait = true; 
                break;
            default: // move to center of the room
                targetVector = new Vector3(cust.transform.position.x, 
                    cust.transform.position.y, 
                    bar.MiddleOfTheRoom.transform.position.z);
                    StatusID = 1;
                break;
        }
        return targetVector;
    }

    public void SwitchRoom()
    {
        if (Seat)
        {
            bar.Seats.Add(Seat);
            bar.custAtBar.Remove(cust);
        }        
    }
}
