using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/DeactivatingCharacter")]
public class DeactivatingCharacter : Action {
    public override bool Act(myCharacterController controller)
    {
        controller.ActionID = -1;//cuz it still will be increased by 1 after comleting this action
        controller.behaviour = MyBehavioursCollection.Instance.Customer.EnterReception;
        controller.gameObject.SetActive(false);
        return true;
    }
}
