using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustReception : ICustBehaviour
{
    Customer cust;
    Reception reception;
    Animator animator;
    GameObject Target;
    int targetIndex;

    public void RoomBehaviour<T>(Customer customer, T RoomType, Animator AnimatorComponent)
    {
        cust = customer;
        reception = RoomType as Reception;
        animator = AnimatorComponent;
    }

    public GameObject RoomBehaviour()
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
        return Target;
    }

    public void SwitchRoom()
    {
        reception.OccupiedSpot[targetIndex] = false;
    }

    public GameObject LeaveRoom()
    {
        throw new System.NotImplementedException();
    }
}
