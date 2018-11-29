using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/TargetSetters/TargetObj")]
public class SetToTargetObj : TargetSetter {
    public override Vector3 Set(myCharacterController Character)
    {
        return Character.Focus.TargetObj.position;
    }
}
