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
    public FocusData Focus;
    public float X, Y, Z, AnimationWaitTime, CountDown;
    public int TargetRoomIndex, BehaviourStatusID;
    public string CharacterType;
}

[System.Serializable]
public class RoomSaveData
{
    public int typeAndSizeID;
    public float X, Y, Z;
}


