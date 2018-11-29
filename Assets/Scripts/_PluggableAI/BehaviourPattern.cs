using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Behaviour")]
public class BehaviourPattern : ScriptableObject {

     public Action[] actions;
     public Action ResetFocus;
    //public BehaviourPattern Next;

    public void UpdateBehaviour(myCharacterController Character)
    {
        if (Character.Stats.CountDown < 0 
            && (Character.behaviour != MyBehavioursCollection.Instance.ChangeRoom) 
            && (Character.behaviour != MyBehavioursCollection.Instance.Customer.CountDown_0)
            && (Character.behaviour != MyBehavioursCollection.Instance.Personnel.CountDown_0))
        {
            Character.Stats.CountDownReached_0(Character);
        }            
        else
            DoActions(Character);
    }

    private void DoActions(myCharacterController controller)
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