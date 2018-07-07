using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVer2 {

    public Reception reception;
    public Vector3 Target, Position;
    public ICustBehaviour Behaviour;

    public virtual void EnterTriggerBehaviour(Collider other, Transform transform)
    {
    }
    }
