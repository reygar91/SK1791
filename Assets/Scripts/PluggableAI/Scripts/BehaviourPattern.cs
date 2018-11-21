using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Behaviour")]
public class BehaviourPattern : ScriptableObject {

    public Action[] actions;
    public Action ResetFocus;
    //public BehaviourPattern Next;

    public void UpdateBehaviour(BehaviourController Character)
    {
        if (Character.character.CountDown < 0 && 
            ((Character.behaviour != Character.CountDown_0) && (Character.NextBehaviour != Character.CountDown_0)))
        {
            ResetFocus.Act(Character);
            Character.ActionID = 0;
            Character.Focus.Activity = Reception.Instance;
            Character.Focus.Object = Reception.Instance.EntrancePoint;
            Character.NextBehaviour = Character.CountDown_0;
            Character.behaviour = MyBehavioursCollection.Instance.ChangeRoom;
        }            
        else
            DoActions(Character);
    }

    private void DoActions(BehaviourController controller)
    {
        bool Succeed = actions[controller.ActionID].Act(controller);
        if (Succeed)
            controller.ActionID++;
    }

}


//check if in reception, if true ActId + 2, to skip further steps
//exitRoom
//teleport to targetRoom
//
//MoveToReceptionEntrance
//moveToSpawnPoint
//deactivate