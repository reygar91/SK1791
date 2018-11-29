using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/FocusResetAtReception")]
public class FocusResetAtReception : Action {
    public override bool Act(myCharacterController Character)
    {
        //Reception room = Reception.Instance;
        if (Character.Focus.TargetObj)
        {
            Queue queue = Character.Focus.Activity as Queue;
            queue.OccupiedQueue[Character.Focus.Target.ObjectIndex] = false;
            Character.Focus.TargetObj = null;
        }
        return true;
    }
}
