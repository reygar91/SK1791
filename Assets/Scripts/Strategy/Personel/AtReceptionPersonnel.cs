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
