using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtReceptionPersonnel : ICharBehaviour
{
    //Personnel cust;
    MonoCharacter MC;
    Reception room;
    Vector3 targetVector;
    int StatusID, targetIndex;
    GameObject Target;

    public AtReceptionPersonnel(MonoCharacter monoCharacter)
    {
        MC = monoCharacter;
        //cust = MC.character as Customer;
        room = Reception.Instance;
        //Debug.Log(MC.name + "=> CustReception called/ behaviour data = " + cust.behaviourData);
        //if (MC.character.behaviourData != null)
        //{
        //    targetIndex = cust.behaviourData.OOI_Index;
        //    Target = room.WaitInLinePoints[targetIndex];
        //    room.OccupiedSpot[targetIndex] = true;
        //    StatusID = cust.behaviourData.StateID;
        //    targetVector = new Vector3(cust.behaviourData.TargetX, MC.transform.position.y, cust.behaviourData.TargetZ);
        //    cust.behaviourData = null;
        //}
        //Debug.Log("new CustReception => StatusID = " + StatusID); 
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
        switch (StatusID)
        {
            case 1:
                targetVector = new Vector3(room.Doors.transform.position.x, MC.transform.position.y, MC.transform.position.z);
                StatusID = 2;
                break;
            case 2:
                targetVector = room.Doors.transform.position;
                StatusID = 3;
                break;
            case 3:
                MC.transform.position = targetRoom.Doors.transform.position;
                targetVector = targetRoom.Doors.transform.position;
                targetVector.z -= 1.5f;
                StatusID = 4;
                break;
            case 4:
                //empty, so to do nothing untill he enters to a new room
                break;
            default:
                targetVector = new Vector3(MC.transform.position.x, MC.transform.position.y, room.EntrancePoint.transform.position.z);
                StatusID = 1;
                //SwitchRoom(); //Debug.Log("Change Room");
                break;
        }
        return targetVector;
    }

    public Vector3 RoomBehaviour()
    {
        //targetIndex = 0;
        switch (StatusID)
        {
            case 10:
                MC.character.state = Character.State.Animation;
                MC.character.AnimationWaitTime = 5.0f; Debug.Log("PersAtReception => " + targetIndex);
                targetIndex++;
                break;
            default:
                targetVector = new Vector3(room.Doors.transform.position.x, MC.transform.position.y, MC.transform.position.z);
                StatusID = 10;
                break;
        }       
        return targetVector;
    }
}
