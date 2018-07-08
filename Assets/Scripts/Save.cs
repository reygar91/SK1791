using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save {

    public List<CharData> Characters = new List<CharData>();
    //public List<Room> Rooms = new List<Room>();

    public int gold = 0;
    public int time = 0;

}

[System.Serializable]
public class CharData
{

    public Character.State state, prevState;
    public string Behaviour;
    public int BehaviourStateID;
    public float X, Y, Z, TargetX, TargetY, AnimationWaitTime, CountDown;

}
