using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/EnterReceptionQueue")]
public class EnterReceptionQueue : Action {

    public override bool Act(BehaviourController Character)
    {
        bool result = false;
        Reception reception = Reception.Instance;
        if (!Character.Focus.Object) //possibly not neccesary any more
        {
            for (int i = 0; i < 5; i++)
            {
                if (reception.OccupiedSpot[i] == false)
                {
                    Character.Focus.Object = reception.WaitInLinePoints[i];
                    reception.OccupiedSpot[i] = true;
                    Character.Focus.Index = i;
                    i = 5;
                    result = true;
                    Vector3 Target = Character.Focus.Object.transform.position;
                    //float MovementLine = reception.MiddleOfTheRoom.transform.position.z;
                    Character.mover.StartMovement(Target);
                }
            }
        }
        //Debug.Log("EnterReceptionQueue Executed");
        return result;
    }
}
