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

    public CustBar(Customer customer, Bar RoomType)
    {
        cust = customer;
        bar = RoomType;
        StatusID = 0;
    }   

    public Vector3 LeaveRoom()
    {
        //Vector3 targetVector;
        switch (StatusID)
        {
            case 101:
                targetVector = new Vector3(bar.Doors.transform.position.x, bar.Doors.transform.position.y, bar.MiddleOfTheRoom.transform.position.z);
                if (cust.hasReachedTarget(targetVector))
                {
                    StatusID = 102;
                }
                break;
            case 102:
                targetVector = bar.Doors.transform.position;
                if (cust.hasReachedTarget(targetVector))
                {
                    cust.transform.position = cust.reception.InternalDoor.transform.position;
                    targetVector = new Vector3(cust.transform.position.x, cust.transform.position.y, cust.reception.EntrancePoint.transform.position.z);
                    StatusID = 103;
                }
                break;
            case 103:                
                targetVector = new Vector3(cust.transform.position.x, cust.transform.position.y, cust.reception.EntrancePoint.transform.position.z);
                break;
            default://move to the center of the room
                targetVector = new Vector3(cust.transform.position.x, cust.transform.position.y, bar.MiddleOfTheRoom.transform.position.z);
                if (cust.hasReachedTarget(targetVector)) {
                    StatusID = 101;
                }
                break;
        }
        return targetVector;
    }

    public Vector3 RoomBehaviour()
    {
        //Vector3 targetVector;
        switch (StatusID)
        {
            case 1:
                if (bar.Seats.Count != 0 && !Seat)
                {
                    int RandomSeat = Random.Range(0, bar.Seats.Count);
                    Seat = bar.Seats[RandomSeat];
                    bar.Seats.Remove(Seat);
                    targetVector = Seat.transform.position;
                    StatusID = 2;
                }
                break;
            case 2:
                if (cust.hasReachedTarget(targetVector))
                {
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
                }
                break;
            case 3:
                break;
            default:
                targetVector = new Vector3(cust.transform.position.x, 
                    cust.transform.position.y, 
                    bar.MiddleOfTheRoom.transform.position.z);
                if (cust.hasReachedTarget(targetVector))
                {
                    StatusID = 1;
                }
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
