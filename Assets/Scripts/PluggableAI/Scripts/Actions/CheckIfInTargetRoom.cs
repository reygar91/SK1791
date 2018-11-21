using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/CheckIfInTargetRoom")]
public class CheckIfInTargetRoom : Action
{
    public override bool Act(BehaviourController Character)
    {
        Room Room = Character.Focus.Object.GetComponentInParent<Room>();
        if (Room == Character.Focus.Room)
        {
            //controller.Focus.TeleportDestination = null;
            Character.ActionID += 2;//skip next 2 steps in algorithm
        }
        return true;
    }
}
