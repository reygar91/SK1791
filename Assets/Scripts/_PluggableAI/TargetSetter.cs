using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetSetter : ScriptableObject
{
    public abstract Vector3 Set(myCharacterController Character);
}
