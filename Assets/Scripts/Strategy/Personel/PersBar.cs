using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersBar : ICharBehaviour
{
    Personnel pers;
    MonoCharacter MC, custMC;
    Bar room;
    Vector3 targetVector;
    int StatusID, targetIndex;
    //Customer cust;

    public PersBar(MonoCharacter monoCharacter)
    {
        MC = monoCharacter;
        pers = MC.character as Personnel;
        room = MC.character.CurrentRoom as Bar;
        if (pers.behaviourData != null)
        {
            targetIndex = pers.behaviourData.OOI_Index;
            //Seat = room.FindSeat(targetIndex);
            //room.AvailableSeats.Remove(Seat);
            StatusID = pers.behaviourData.StateID;
            targetVector = new Vector3(pers.behaviourData.TargetX, MC.transform.position.y, pers.behaviourData.TargetZ);
            pers.behaviourData = null;
            //Debug.Log(MC.name + "=> CustBar with behaviourData called");
        }
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

    public Vector3 ChangeRoom(Room targetRoom)
    {
        throw new System.NotImplementedException();
    }


    public Vector3 RoomBehaviour()
    {
        Vector3 persPos = MC.transform.position;
        Vector3 custPos;
        Vector3 Middle = room.MiddleOfTheRoom.transform.position;
        switch (StatusID)
        {
            case 1:// cheking if there are any custs to serve or reached previous random target
                if (room.custAtBar.Count != 0)
                {
                    StatusID = 3;
                }
                else
                {
                    StatusID = 2;
                }
                break;
            case 2://set random target to move along the room and change statusID so it was done only once 
                float delta = room.Doors.transform.position.x - Middle.x;//distance from center of the room to the doors, considering doors located right to the center. doors.x > center.x
                float minX = Middle.x - delta;
                float maxX = Middle.x + delta;
                targetVector = new Vector3(Random.Range(minX, maxX), persPos.y, Middle.z);
                StatusID = 1;
                break;
            case 3://setting target to guest X position and changing StatusID, so it is done only once
                int index = Random.Range(0, room.custAtBar.Count);
                custMC = room.custAtBar[index];
                custPos = custMC.transform.position;
                targetVector = new Vector3(custPos.x, persPos.y, Middle.z);
                room.custAtBar.Remove(custMC);
                StatusID = 4;
                break;
            case 4://setting target to guest X,Z position & changing StatusID
                custPos = custMC.transform.position;
                targetVector = new Vector3(custPos.x, persPos.y, custPos.z);
                StatusID = 5;
                break;
            case 5:
                GoldMNGR.Instance.AddGold(150);
                StatusID = 0;
                break;
            default://when just entered the room or just served cust
                targetVector = new Vector3(persPos.x, persPos.y, Middle.z);
                StatusID = 1;
                break;
        }
        return targetVector;
    }
}
