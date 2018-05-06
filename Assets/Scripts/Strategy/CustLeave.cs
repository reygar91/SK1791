using System;
using System.Collections;
using UnityEngine;

public class CustLeave : ICustBehaviour
{
    Customer customer;
    Room room;
    Animator animator;
    GameObject Target;

    public void RoomBehaviour<T>(Customer cust, T RoomType, Animator AnimatorComponent)
    {
        customer = cust;
        //room = RoomType as Room;
        animator = AnimatorComponent;
    }

    public GameObject RoomBehaviour()
    {
        Target = customer.WaypointRecord[customer.WaypointRecord.Length - 1];//Debug.Log("custPos=" + customer.transform.position + " TargetPos=" + Target.transform.position);
        if (customer.transform.position == Target.transform.position)
        {
            if (customer.WaypointRecord.Length == 1)
            {
                customer.gameObject.SetActive(false);
            }
            else
            {
                Array.Resize(ref customer.WaypointRecord, customer.WaypointRecord.Length - 1);
                Target = customer.WaypointRecord[customer.WaypointRecord.Length - 1];
            }
        }
        return Target;
    }
    public void SwitchRoom()
    {
        
    }
}
