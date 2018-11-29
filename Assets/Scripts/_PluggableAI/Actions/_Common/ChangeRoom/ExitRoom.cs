using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/ExitRoom")]
public class ExitRoom : Action {

    public override bool Act(myCharacterController Character)
    {
        //Debug.Log(Character.name + " => " + Character.Stats.state);
        Character.Mover.StartMovement(Vector3.zero, "Exit");
        //Debug.Log(Character.name + " => " + Character.Stats.state);
        //Debug.Log("ExitRoom Executed");
        return true;
    }

}
