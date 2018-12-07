using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/MoveToSpawnPoint")]
public class MoveToSpawnPoint : Action {

    public Transform Target;

    public override bool Act(myCharacterController controller)
    {
       
        controller.Focus.TargetObj = Reception.Instance.TargetCustSpawnPoint();
        controller.Focus.Target.ObjectIndex = controller.Focus.Activity.FindIndexOfInteractionObj(controller.Focus.TargetObj);
        Vector3 Target = controller.Focus.TargetObj.position;
        //float MovementLine = controller.character.CurrentRoom.MiddleOfTheRoom.transform.position.z;
        controller.Mover.StartMovement(Target, "Direct");
        return true;
    }
}
