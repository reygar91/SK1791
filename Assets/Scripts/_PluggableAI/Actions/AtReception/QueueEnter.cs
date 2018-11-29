using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/QueueEnter")]
public class QueueEnter : Action {

    public override bool Act(myCharacterController Character)
    {
        bool result = false;
        System.Predicate<Activity> predicate = FindType;
        Character.Focus.Activity = Character.Focus.Activity.Room.Activities.Find(predicate) as Queue;
        if (!Character.Focus.TargetObj) //possibly not neccesary any more
        {
            Queue queue = Character.Focus.Activity as Queue;
            for (int i = 0; i < 5; i++)
            {
                if (queue.OccupiedQueue[i] == false)
                {
                    Character.Focus.TargetObj = queue.FindInteractionObjByIndex(i);
                    queue.OccupiedQueue[i] = true;
                    Character.Focus.Target.ObjectIndex = i;
                    i = 5;
                    result = true;
                    //Vector3 Target = Character.Focus.TargetObj.position;
                    //float MovementLine = reception.MiddleOfTheRoom.transform.position.z;
                    //Character.Mover.StartMovement(Target, "Direct");
                }
            }
        }
        //Debug.Log("EnterReceptionQueue Executed");
        return result;
    }

    private bool FindType(Activity type)
    {
        return type is Queue;
    }
}
