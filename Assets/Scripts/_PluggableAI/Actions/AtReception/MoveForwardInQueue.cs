using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/MoveForwardInQueue")]
public class MoveForwardInQueue : Action {

    public Action Movement;

    public override bool Act(myCharacterController Character)
    {
        bool result = false;
        if (Character.Focus.Target.ObjectIndex != 0)
        {
            Queue queue = Character.Focus.Activity as Queue;
            if (queue.OccupiedQueue[Character.Focus.Target.ObjectIndex - 1] == false)
            {
                Character.Focus.TargetObj = queue.FindInteractionObjByIndex(Character.Focus.Target.ObjectIndex - 1);

                queue.OccupiedQueue[Character.Focus.Target.ObjectIndex - 1] = true;
                queue.OccupiedQueue[Character.Focus.Target.ObjectIndex] = false;
                Character.Focus.Target.ObjectIndex--;

                Movement.Act(Character);
            }
        }
        else
            result = true;

        return result;
    } 
}
