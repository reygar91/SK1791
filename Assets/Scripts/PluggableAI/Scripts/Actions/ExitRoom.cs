using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/ExitRoom")]
public class ExitRoom : Action {

    public override bool Act(BehaviourController Character)
    {
        //Debug.Log(Character.Focus.Room);
        Vector3 Target = Character.Focus.Room.Doors.transform.position;
        float MovementLine = Character.Focus.Room.MiddleOfTheRoom.transform.position.z;
        Character.mover.StartMovement(Target, MovementLine);
        //Debug.Log("ExitRoom Executed");
        return true;
    }

}
