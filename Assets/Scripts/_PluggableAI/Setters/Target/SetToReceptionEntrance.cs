using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/TargetSetters/ReceptionEntrance")]
public class SetToReceptionEntrance : TargetSetter {
    public override Vector3 Set(myCharacterController Character)
    {
        Character.Focus.Activity = Reception.Instance;
        return Reception.Instance.TargetReceptionEntrance().position;
    }
}
