using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/MoveToSpawnPoint")]
public class MoveToSpawnPoint : Action {
    public override bool Act(BehaviourController controller)
    {
        Vector3 Target = Reception.Instance.SpawnPoint.transform.position;
        //float MovementLine = controller.character.CurrentRoom.MiddleOfTheRoom.transform.position.z;
        controller.mover.StartMovement(Target);
        return true;
    }
}
