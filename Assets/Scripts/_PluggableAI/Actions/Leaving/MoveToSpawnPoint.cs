using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/MoveToSpawnPoint")]
public class MoveToSpawnPoint : Action {

    public Transform Target;

    public override bool Act(myCharacterController controller)
    {
        Vector3 Target = Reception.Instance.TargetCustSpawnPoint().position;
        //float MovementLine = controller.character.CurrentRoom.MiddleOfTheRoom.transform.position.z;
        controller.Mover.StartMovement(Target, "Direct");
        return true;
    }
}
