using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/CheckIfInTargetRoom")]
public class CheckIfInTargetRoom : Action
{
    public override bool Act(myCharacterController Character)
    {
        if (Character.Mover.IsInActivityRoom())
        {
            Character.ActionID += 2;//skip next 2 steps in algorithm
        }
        return true;
    }
}
