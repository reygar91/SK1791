using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reception : Activity {

    public static Reception Instance;

    public GameObject SpawnPoint, EntrancePoint;
    public GameObject[] WaitInLinePoints;
    public bool[] OccupiedSpot;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
        Room.MiddleOfTheRoom = EntrancePoint;
        //AvailablePersonel = new BehaviourController[1];
    }

}
