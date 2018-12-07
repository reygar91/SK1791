using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/TargetSetters/ReceptionEntrance")]
public class SetToReceptionEntrance : TargetSetter {
    public override Vector3 Set(myCharacterController Character)
    {
        Character.Focus.Activity = Reception.Instance;
        Character.Focus.TargetObj = Reception.Instance.TargetReceptionEntrance();
        Character.Focus.Target.ObjectIndex = Reception.Instance.FindIndexOfInteractionObj(Character.Focus.TargetObj);
        return Character.Focus.TargetObj.position;
    }
}
