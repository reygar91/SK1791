using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reception : Activity {

    public static Reception Instance;

    //public Transform SpawnPoint, EntrancePoint;
    //public Transform[] Queue;
    //public bool[] OccupiedQueue;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
        Room.CustomAwake();
    }

    public Transform TargetReceptionEntrance()
    {
        return CustomerInteractionObjects[0].transform;
    }

    public Transform TargetCustSpawnPoint()
    {
        return CustomerInteractionObjects[1].transform;
    }

    public override void Load(int ObjectIndex, myCharacterController CC)
    {
    }

}
