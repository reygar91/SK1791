using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/TeleportToTargetRoom")]
public class TeleportToTargetRoom : Action
{
    public override bool Act(BehaviourController Character)
    {
        Character.transform.position = Character.Focus.Activity.Room.Doors.transform.position;
        
        //controller.Focus.TeleportDestination = null;
        //controller.character.ApplyRoomBehaviour(controller);
        return true;
    }
}
