using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : IPersJob {

    public static List<Customer> CustomersAtBar = new List<Customer>();

    public GameObject JobInstructions(bool isReachedTarget)
    {
        if (CustomersAtBar.Count !=0 && isReachedTarget)
        {            
            CustomersAtBar.Remove(CustomersAtBar[0]); //Debug.Log("remove" + CustomersAtBar[0]);
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
