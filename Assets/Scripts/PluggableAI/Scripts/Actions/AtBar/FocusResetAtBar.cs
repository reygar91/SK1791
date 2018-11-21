using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "PluggableAI/Actions/FocusResetAtBar")]
public class FocusResetAtBar : Action {
    public override bool Act(BehaviourController Character)
    {
        
        //Bar room = controller.Focus.CurrentRoom as Bar;
        if (Character.Focus.Object) //which is seat
        {
            BarMNGR.Instance.AvailableSeats.Add(Character.Focus.Object.transform);
            Character.Focus.Activity.Customers.Remove(Character);
            Character.Focus.Object = null;
        }
        return true;
    }

    
}
