using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/MoveToTarget")]
public class MoveToTarget : Action
{
    public TargetSetter Target;
    public string MovementType;

    public override bool Act(myCharacterController Character)
    {
        //Vector3 Target = Character.Focus.TargetObj.position;
        //float MovementLine = Character.mover.Room.MiddleOfTheRoom.transform.position.z;
        Character.Mover.StartMovement(Target.Set(Character), MovementType);
        //Debug.Log("MoveToTarget Executed");
        return true;
    }
}
