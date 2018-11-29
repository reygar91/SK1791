using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue : Activity {

    //public static Reception Instance;

    //public Transform SpawnPoint, EntrancePoint;
    //public Transform[] Queue;
    public bool[] OccupiedQueue;

    //protected override void Awake()
    //{
    //    base.Awake();
    //    //Instance = this;
    //    //Room.MiddleOfTheRoom = EntrancePoint;
    //    //AvailablePersonel = new BehaviourController[1];
    //}

    public override void Load(int ObjectIndex, myCharacterController CC)
    {
        OccupiedQueue[ObjectIndex] = true;
    }

}
