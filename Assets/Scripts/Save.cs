using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save {

    public CharSaveData[] Characters;
    public RoomSaveData[] Rooms;
    public string[] ActiveEvents;

    public int gold = 0;
    public int time = 0;
}

[System.Serializable]
public class CharSaveData
{
    public Character.State state, prevState;
    public BehaviourData behaviour;
    public float X, Y, Z, AnimationWaitTime, CountDown;
    public int TargetRoomIndex;
    public string CharacterType;
}

[System.Serializable]
public class BehaviourData
{
    //public string name;
    public int StateID, OOI_Index;
    public float TargetX, TargetZ;
    //public GameObject ObjectOfInterest; // GO can not be serialized, need to change bar to look like reception, 2 arrays 1 with GO another with availability index
}

[System.Serializable]
public class RoomSaveData
{
    public int typeAndSizeID;
    public float X, Y, Z;
}


