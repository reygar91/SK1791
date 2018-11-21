using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/MoveToReceptionDesk")]
public class MoveToReceptionDesk : Action {
    public override bool Act(BehaviourController controller)
    {
        Reception reception = Reception.Instance;
        Vector3 Target = reception.WaitInLinePoints[0].transform.position + new Vector3(0.5f, 0, 0);
        float MovementLine = reception.Room.MiddleOfTheRoom.transform.position.z;
        controller.mover.StartMovement(Target, MovementLine);
        return true;
    }    
}
