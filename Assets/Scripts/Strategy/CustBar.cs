using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustBar : ICustBehaviour
{
    Customer cust;
    Bar bar;
    Animator animator;
    //GameObject Target;
    GameObject Seat;
    int StatusID;

    public CustBar(Customer customer, Bar RoomType)
    {
        cust = customer;
        bar = RoomType;
        //animator = AnimatorComponent;
        StatusID = 0;
    }
    
    public Vector3 RoomBehaviour()
    {
        if (bar.Seats.Count != 0 && !Seat)
        {
            int RandomSeat = UnityEngine.Random.Range(0, bar.Seats.Count);
            Seat = bar.Seats[RandomSeat];
            bar.Seats.Remove(Seat);
        }
        return Seat.transform.position;
    }

    public void SwitchRoom()
    {
        bar.Seats.Add(Seat);
    }

    public Vector3 LeaveRoom()
    {
        Vector3 targetVector;
        switch (StatusID)
        {
            case 1:
                targetVector = new Vector3(bar.Doors.transform.position.x, bar.Doors.transform.position.y, bar.MiddleOfTheRoom.transform.position.z);
                if (cust.hasReachedTarget(targetVector))
                {
                    StatusID = 2;
                }
                break;
            case 2:
                targetVector = bar.Doors.transform.position;
                if (cust.hasReachedTarget(targetVector))
                {
                    cust.transform.position = cust.reception.InternalDoor.transform.position;
                    targetVector = new Vector3(cust.transform.position.x, cust.transform.position.y, cust.reception.EntrancePoint.transform.position.z);
                    StatusID = 3;
                }
                break;
            case 3:                
                targetVector = new Vector3(cust.transform.position.x, cust.transform.position.y, cust.reception.EntrancePoint.transform.position.z);
                break;
            default:
                targetVector = new Vector3(cust.transform.position.x, cust.transform.position.y, bar.MiddleOfTheRoom.transform.position.z);
                if (cust.hasReachedTarget(targetVector)) {
                    StatusID = 1;
                }
                break;
        }
        return targetVector;
    }
}
