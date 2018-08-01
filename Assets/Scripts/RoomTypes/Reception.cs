using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reception : Room {

    public static Reception Instance;

    public GameObject SpawnPoint, EntrancePoint;
    public GameObject[] WaitInLinePoints;
    public bool[] OccupiedSpot;

    private void Awake()
    {
        Instance = this;
    }

}
