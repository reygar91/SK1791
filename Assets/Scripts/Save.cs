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
    public myCharacterStats.State state, prevState;
    public FocusTarget Focus;
    public float X, Y, Z, AnimationWaitTime, CountDown;
    public int ActionID;
    public myCharacterStats Stats;
    public string behaviour;
    //public string CharacterType;
}

[System.Serializable]
public class RoomSaveData
{
    public int typeAndSizeID;
    public float X, Y, Z;
}


