﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/NoAction")]
public class NoAction : Action {
    public override bool Act(BehaviourController controller)
    {
        return false;
    }

}
