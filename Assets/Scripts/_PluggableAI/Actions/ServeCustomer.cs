using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/ServeCustomer")]
public class ServeCustomer : Action {

    public override bool Act(myCharacterController Character)
    {
        GoldMNGR.Instance.AddGold(150);
        Character.Focus.CC = null;
        return true;
    }

}
