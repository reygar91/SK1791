using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/ResetBehaviourActionID")]
public class ResetBehaviourActionID : Action {
    public override bool Act(myCharacterController Character)
    {
        Character.ActionID = -1;
        return true;
    }
}
