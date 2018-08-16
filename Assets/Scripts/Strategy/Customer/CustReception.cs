using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustReception : ICharBehaviour
{
    Customer cust;
    MonoCharacter MC;
    Reception room;
    Vector3 targetVector;
    int StatusID, targetIndex;
    GameObject Target;

    public CustReception(MonoCharacter monoCharacter)
    {
        MC = monoCharacter;
        cust = MC.character as Customer;        
        room = Reception.Instance;
        //Debug.Log(MC.name + "=> CustReception called/ behaviour data = " + cust.behaviourData);
        if (cust.behaviourData != null)
        {
            targetIndex = cust.behaviourData.OOI_Index;
            Target = room.WaitInLinePoints[targetIndex];
            room.OccupiedSpot[targetIndex] = true;
            StatusID = cust.behaviourData.StateID;
            targetVector = new Vector3(cust.behaviourData.TargetX, MC.transform.position.y, cust.behaviourData.TargetZ);
            cust.behaviourData = null;
        }
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
        if (cust.CountDown < 0)
        {
            switch (StatusID)
            {
                case 10:
                    targetVector = room.EntrancePoint.transform.position;
                    StatusID = 11;
                    break;
                case 11:
                    targetVector = room.SpawnPoint.transform.position;
                    StatusID = 12;
                    break;
                case 12:
                    MC.gameObject.SetActive(false);
                    break;
                default:
                    targetVector = new Vector3(MC.transform.position.x, MC.transform.position.y, room.EntrancePoint.transform.position.z);
                    StatusID = 10;
                    SwitchRoom(); //Debug.Log("leave room default switch");
                    break;
            }
        }
        else if (!Target)
        {
            for (int i=0; i < 5; i++)
            {
                if(room.OccupiedSpot[i] == false)
                {
                    Target = room.WaitInLinePoints[i];
                    targetVector = Target.transform.position;
                    room.OccupiedSpot[i] = true;
                    targetIndex = i;
                    i = 5;
                }
            }
        } else if (targetIndex !=0 && room.OccupiedSpot[targetIndex-1] == false)
        {
            Target = room.WaitInLinePoints[targetIndex-1];
            targetVector = Target.transform.position;
            room.OccupiedSpot[targetIndex-1] = true;
            room.OccupiedSpot[targetIndex] = false;
            targetIndex--;
        }
        return targetVector;
    }

    private void SwitchRoom()
    {
        if (Target)
        {
            room.OccupiedSpot[targetIndex] = false; //Debug.Log(cust.name + "_triggers");
            Target = null;
        }
    }
    
}
