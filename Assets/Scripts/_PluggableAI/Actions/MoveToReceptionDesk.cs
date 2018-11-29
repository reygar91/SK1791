using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/MoveToReceptionDesk")]
public class MoveToReceptionDesk : Action {
    public override bool Act(myCharacterController Character)
    {
        Queue queue = Character.Focus.Activity.Room.GetComponent<Queue>();

        Vector3 Target = queue.FindInteractionObjByIndex(0).position + new Vector3(-2f, 0, 0);
        Character.Mover.StartMovement(Target, "");
        return true;
    }    
}
