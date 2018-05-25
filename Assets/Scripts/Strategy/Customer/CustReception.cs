using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustReception : ICustBehaviour
{
    Customer cust;
    Reception reception;
    Animator animator;
    GameObject Target;
    int targetIndex, StatusID;


    public CustReception(Customer customer, Reception RoomType)
    {
        cust = customer;
        reception = RoomType;
        //animator = AnimatorComponent;
        StatusID = 0;
    }

    public Vector3 LeaveRoom()
    {
        Vector3 targetVector;
        switch (StatusID)
        {
            case 1:
                targetVector = cust.reception.EntrancePoint.transform.position;
                StatusID = 2;
                break;
            case 2:
                targetVector = cust.reception.SpawnPoint.transform.position;
                cust.gameObject.SetActive(false);
                break;
            default:
                targetVector = new Vector3(cust.transform.position.x, cust.transform.position.y, reception.EntrancePoint.transform.position.z);
                StatusID = 1;
                if (Target)
                {
                    SwitchRoom();
                }
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

    public void SwitchRoom()
    {
        reception.OccupiedSpot[targetIndex] = false;
    }
    
}
