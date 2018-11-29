using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "PluggableAI/Actions/FocusResetAtBar")]
public class FocusResetAtBar : Action {
    public override bool Act(myCharacterController Character)
    {
        
        //Bar room = controller.Focus.CurrentRoom as Bar;
        if (Character.Focus.TargetObj) //which is seat
        {
            BarMNGR.Instance.AvailableSeats.Add(Character.Focus.TargetObj);
            Character.Focus.Activity.Customers.Remove(Character);
            Character.Focus.TargetObj = null;
        }
        return true;
    }

    
}
