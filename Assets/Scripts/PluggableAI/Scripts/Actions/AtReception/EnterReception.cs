using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/EnterReception")]
public class EnterReception : Action
{
    public override bool Act(BehaviourController Character)
    {
        Reception reception = Reception.Instance;
        Character.Focus.Room = reception.Room; //Debug.Log(reception.Room);
        Vector3 Target = reception.EntrancePoint.transform.position;
        //float MovementLine = reception.MiddleOfTheRoom.transform.position.z;
        Character.mover.StartMovement(Target);
        //Debug.Log("EnterReception Executed");
        return true;
    }
}
