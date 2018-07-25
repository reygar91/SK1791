using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustBar : ICharBehaviour
{
    Customer cust;
    MonoCharacter MC;
    Bar room;
    Vector3 targetVector;
    int StatusID, targetIndex;
    GameObject Seat;

    public CustBar(Customer customer)
    {        
        cust = customer;
        MC = cust.monoCharacter;
        room = MC.CurrentRoom as Bar;
        //Debug.Log(MC.name + "=> CustBar called");
        if (cust.behaviourData != null)
        {
            targetIndex = cust.behaviourData.OOI_Index;
            Seat = room.FindSeat(targetIndex);
            StatusID = cust.behaviourData.StateID;
            targetVector = new Vector3(cust.behaviourData.TargetX, MC.transform.position.y, cust.behaviourData.TargetZ);
            cust.behaviourData = null;
            //Debug.Log(MC.name + "=> CustBar with behaviourData called");
        }
        //Debug.Log("new CustBar => StatusID = " + StatusID);
    }

    public Vector3 ChangeRoom(Room targetRoom)
    {
        Transform Doors, Middle, Char;
        Doors = room.Doors.transform;
        Middle = room.MiddleOfTheRoom.transform;
        Char = MC.transform;
        switch (StatusID)
        {
            case 1:
                targetVector = new Vector3(Doors.position.x, Doors.position.y, Middle.position.z);
                StatusID = 2;
                break;
            case 2:
                targetVector = Doors.position;
                StatusID = 3;
                break;
            case 3:
                Char.position = targetRoom.Doors.transform.position;
                targetVector = targetRoom.Doors.transform.position;
                targetVector.z -= 1.5f;
                StatusID = 4;
                break;
            case 4:
                //empty, so to do nothing untill he enters to a new room
                break;
            default://move to the center of the room
                targetVector = new Vector3(Char.position.x, Char.position.y, Middle.position.z);
                StatusID = 1;
                SwitchRoom();
                break;
        }
        return targetVector;
    }

    public BehaviourData BehaviourData
    {
        get
        {
            BehaviourData data = new BehaviourData
            {
                OOI_Index = targetIndex,
                StateID = StatusID,
                TargetX = targetVector.x,
                TargetZ = targetVector.z
            };
            return data;
        }
    }

    public Vector3 RoomBehaviour()
    {
        switch (StatusID)
        {
            case 10: //find a seat and select it as new target
                if (room.AvailableSeats.Count != 0 && !Seat)
                {
                    int RandomSeat = Random.Range(0, room.AvailableSeats.Count);
                    Seat = room.AvailableSeats[RandomSeat];
                    room.AvailableSeats.Remove(Seat);
                    targetVector = Seat.transform.position;
                    targetIndex = room.SeatIndex(Seat);
                    StatusID = 11;
                }
                break;
            case 11: //adding cust to custAtBar list + setting his orientation to correct side
                    room.custAtBar.Add(cust);
                    int startIndex = Seat.name.IndexOf("_");
                    string orientation = Seat.name.Substring(startIndex + 1);
                    switch (orientation)
                    {
                        case "Right":
                            MC.transform.rotation = new Quaternion(0, -1, 0, 0);//facing from right to left
                            break;
                        case "Left":
                            MC.transform.rotation = new Quaternion(0, 0, 0, 1);//facing from left to right
                            break;
                    }
                    StatusID = 12;
                break;
            case 12://here we play animation for cust.AnimationTime duration
                cust.AnimationWaitTime = 5f; //Debug.Log(StatusID);
                cust.state = Character.State.Animation;             
                StatusID = 13;
                break;
            case 13://after main action finished playing idle animation till the rest of patience
                cust.AnimationWaitTime = cust.CountDown; //Debug.Log(StatusID);
                //cust.Wait = true; 
                break;
            default: // move to center of the room
                targetVector = new Vector3(MC.transform.position.x, MC.transform.position.y, room.MiddleOfTheRoom.transform.position.z);
                StatusID = 10;
                break;
        }
        return targetVector;
    }

    private void SwitchRoom()
    {
        if (Seat)
        {
            room.AvailableSeats.Add(Seat);
            room.custAtBar.Remove(cust);
        }        
    }
}
