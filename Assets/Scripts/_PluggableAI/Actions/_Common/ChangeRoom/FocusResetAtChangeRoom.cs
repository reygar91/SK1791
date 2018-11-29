using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/FocusResetAtChangeRoom")]
public class FocusResetAtChangeRoom : Action {
    public override bool Act(myCharacterController controller)
    {
        controller.NextBehaviour.ResetFocus.Act(controller);
        return true;
    }
}
