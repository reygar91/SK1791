using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustReception : ICustBehaviour
{
    Customer cust;
    Reception reception;
    Animator animator;
    GameObject Target, gameObject;
    Vector3 targetVector;
    Transform transform;
    int targetIndex, StatusID;


    public CustReception(Customer customer)
    {
        cust = customer;
        reception = Reception.instance;
        //animator = AnimatorComponent;
        StatusID = 0;
        transform = cust.monoCharacter.transform;
        gameObject = cust.monoCharacter.gameObject;
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
                targetVector = reception.EntrancePoint.transform.position;
                StatusID = 2;
                break;
            case 2:
                targetVector = reception.SpawnPoint.transform.position;
                StatusID = 3;
                break;
            case 3:
                gameObject.SetActive(false);
                break;
            default:
                targetVector = new Vector3(transform.position.x, transform.position.y, reception.EntrancePoint.transform.position.z);
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
                if(reception.OccupiedSpot[i] == false)
                {
                    Target = reception.WaitInLinePoints[i];
                    reception.OccupiedSpot[i] = true;
                    targetIndex = i;
                    i = 5;
                }
            }
        } else if (targetIndex !=0 && reception.OccupiedSpot[targetIndex-1] == false)
        {
            Target = reception.WaitInLinePoints[targetIndex-1]; 
            reception.OccupiedSpot[targetIndex-1] = true;
            reception.OccupiedSpot[targetIndex] = false;
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
            reception.OccupiedSpot[targetIndex] = false; //Debug.Log(cust.name + "_triggers");
            Target = null;
        }
    }
    
}
