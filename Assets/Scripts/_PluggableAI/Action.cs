﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject {

    public abstract bool Act(myCharacterController Character);

    //public Action Move;
}
