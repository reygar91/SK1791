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
public class FocusData
{
    public int Index;
    public bool NotSet = false;
    [System.NonSerialized]
    public MonoCharacter MC;
    [System.NonSerialized]
    public GameObject Object;

    public FocusData SaveFocusData()
    {
        if (Object)
        {
            NotSet = true;
        }
        if (MC)
        {
            NotSet = true;
            Index = CharacterMNGR.Instance.ActiveMC.IndexOf(MC);
            Debug.Log(MC.name + " with index " + Index + " saved");
        }
        return this;
    }
    //public GameObject ObjectOfInterest; // GO can not be serialized, need to change bar to look like reception, 2 arrays 1 with GO another with availability index
}

[System.Serializable]
public class RoomSaveData
{
    public int typeAndSizeID;
    public float X, Y, Z;
}


