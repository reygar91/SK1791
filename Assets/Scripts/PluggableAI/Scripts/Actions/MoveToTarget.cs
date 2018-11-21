using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/MoveToTarget")]
public class MoveToTarget : Action
{
    public override bool Act(BehaviourController Character)
    {
        Vector3 Target = Character.Focus.Object.transform.position;
        float MovementLine = Character.Focus.Room.MiddleOfTheRoom.transform.position.z;
        Character.mover.StartMovement(Target, MovementLine);
        //Debug.Log("MoveToTarget Executed");
        return true;
    }
}
