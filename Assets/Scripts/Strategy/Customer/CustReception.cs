using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustReception : ICharBehaviour
{
    Customer cust;
    //Reception reception;
    MonoCharacter MC;
    GameObject Target;
    Vector3 targetVector;
    int targetIndex, StatusID;


    public CustReception(Customer customer)
    {
        cust = customer;
        //reception = Reception.instance;
        //animator = AnimatorComponent;
        StatusID = 0;
        MC = cust.monoCharacter;
    }

    public Vector3 ChangeRoom(Room targetRoom)
    {
        switch (StatusID)
        {
            case 10:
                targetVector = new Vector3(Reception.instance.Doors.transform.position.x, MC.transform.position.y, MC.transform.position.z);
                StatusID = 11;
                break;
            case 11:
                targetVector = Reception.instance.Doors.transform.position;
                StatusID = 12;
                break;
            case 12:
                MC.transform.position = targetRoom.Doors.transform.position;
                targetVector = targetRoom.Doors.transform.position;
                targetVector.z -= 1.5f;
                StatusID = 13;
                break;
            case 13:
                break;
            default:
                targetVector = new Vector3(MC.transform.position.x, MC.transform.position.y, Reception.instance.EntrancePoint.transform.position.z);
                StatusID = 10;
                SwitchRoom(); //Debug.Log("leave room default switch");
                break;
        }
        return targetVector;
    }

    public int GetStatusID()
    {
        return StatusID;
    }

    public Vector3 LeaveRoom()
    {
        switch (StatusID)
        {
            case 1:
                targetVector = Reception.instance.EntrancePoint.transform.position;
                StatusID = 2;
                break;
            case 2:
                targetVector = Reception.instance.SpawnPoint.transform.position;
                StatusID = 3;
                break;
            case 3:
                MC.gameObject.SetActive(false);
                break;
            default:
                targetVector = new Vector3(MC.transform.position.x, MC.transform.position.y, Reception.instance.EntrancePoint.transform.position.z);
                StatusID = 1;
                SwitchRoom(); //Debug.Log("leave room default switch");
                break;
        }
        return targetVector;
    }

    public Vector3 RoomBehaviour()
    {
        if (!Target)
        {
            for (int i=0; i < 5; i++)
            {
                if(Reception.instance.OccupiedSpot[i] == false)
                {
                    Target = Reception.instance.WaitInLinePoints[i];
                    Reception.instance.OccupiedSpot[i] = true;
                    targetIndex = i;
                    i = 5;
                }
            }
        } else if (targetIndex !=0 && Reception.instance.OccupiedSpot[targetIndex-1] == false)
        {
            Target = Reception.instance.WaitInLinePoints[targetIndex-1];
            Reception.instance.OccupiedSpot[targetIndex-1] = true;
            Reception.instance.OccupiedSpot[targetIndex] = false;
            targetIndex--;
        }
        return Target.transform.position;
    }

    public void SetStatusID(int ID)
    {
        StatusID = ID;
    }

    public void SwitchRoom()
    {
        if (Target)
        {
            Reception.instance.OccupiedSpot[targetIndex] = false; //Debug.Log(cust.name + "_triggers");
            Target = null;
        }
    }
    
}
