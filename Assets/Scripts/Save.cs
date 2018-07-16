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
    public BehaviourData behaviour;
    public float X, Y, Z, TargetX, TargetZ, AnimationWaitTime, CountDown;
}

[System.Serializable]
public class BehaviourData
{
    //public string name;
    public int StateID;
    public GameObject ObjectOfInterest; // GO can not be serialized, need to change bar to look like reception, 2 arrays 1 with GO another with availability index
}


