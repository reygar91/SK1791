using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/ActivateNextBehaviour")]
public class ActivateNextBehaviour : Action {
    public override bool Act(BehaviourController Character)
    {
        Character.Focus.Room = Character.Focus.Activity.Room;
        Character.behaviour = Character.NextBehaviour;
        Character.NextBehaviour = null;
        Character.ActionID = -1;
        return true;
    }
}
