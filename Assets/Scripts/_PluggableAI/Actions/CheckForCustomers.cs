using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/CheckForCustomers")]
public class CheckForCustomers : Action {

    public Action IdleAction;

    public override bool Act(myCharacterController Character)
    {
        bool result = false;

        if (Character.Focus.Activity.Customers.Count != 0)
        {
            int index = Random.Range(0, Character.Focus.Activity.Customers.Count);
            Character.Focus.CC = Character.Focus.Activity.Customers[index];
            //Target.x = Focus.MC.transform.position.x;
            Character.Focus.Activity.Customers.Remove(Character.Focus.CC);
            Character.Focus.CC.Stats.CountDown += 5;
            Character.Mover.StartMovement(Character.Focus.CC.transform.position, "");
            result = true;
        }
        else
            IdleAction.Act(Character);

        return result;
    }
}
