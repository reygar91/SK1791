using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : IPersJob {

    public static List<Customer> CustomersAtBar = new List<Customer>();

    public GameObject JobInstructions(bool hasReachedTarget)
    {
        if (CustomersAtBar.Count !=0 && hasReachedTarget)
        {            
            CustomersAtBar.Remove(CustomersAtBar[0]); //Debug.Log("remove" + CustomersAtBar[0]);
            GoldManager.AddGold(50);
        }
        if (CustomersAtBar.Count !=0)
        {            
            return CustomersAtBar[0].gameObject; //Debug.Log("Target" + CustomersAtBar[0]);
        }
        else
        {            
            return null; //Debug.Log("Returning null");
        }                       
    }

}
