using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/FocusResetAtReception")]
public class FocusResetAtReception : Action {
    public override bool Act(BehaviourController Character)
    {
        Reception room = Reception.Instance;
        if (Character.Focus.Object)
        {
            room.OccupiedSpot[Character.Focus.Index] = false;
            Character.Focus.Object = null;
        }
        return true;
    }
}
