using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/CheckIfInReception")]
public class CheckIfInReception : Action {
    public override bool Act(BehaviourController Character)
    {
        Character.Focus.Activity = Reception.Instance;
        Character.Focus.Object = Reception.Instance.EntrancePoint;        
        //controller.Focus.TeleportDestination = Reception.Instance.Doors.transform;
        if (Character.Focus.Room == Reception.Instance.Room)
        {
            //controller.Focus.TeleportDestination = null;
            Character.ActionID += 2;//skip next 2 steps in algorithm
        }

        return true;
    }
}
