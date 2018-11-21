using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/MoveForwardInQueue")]
public class MoveForwardInQueue : Action {
    public override bool Act(BehaviourController Character)
    {
        bool result = false;
        Reception reception = Reception.Instance; 
        if (Character.Focus.Index != 0)
        {
            if (reception.OccupiedSpot[Character.Focus.Index - 1] == false)
            {
                Character.Focus.Object = reception.WaitInLinePoints[Character.Focus.Index - 1];

                reception.OccupiedSpot[Character.Focus.Index - 1] = true;
                reception.OccupiedSpot[Character.Focus.Index] = false;
                Character.Focus.Index--;
                Vector3 Target = Character.Focus.Object.transform.position;
                float MovementLine = Character.transform.position.z;
                Character.mover.StartMovement(Target, MovementLine);
            }
        }
        else
            result = true;
            
        return result;
    } 
}
