using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reception : Room {

    public GameObject SpawnPoint, EntrancePoint;

    public static Reception Instance;

    public GameObject[] WaitInLinePoints;
    public bool[] OccupiedSpot;


    private void Awake()
    {
        //roomsList.Add(this); // later this should be removed, cuz reception will be added after every load
        Instance = this;
    }

    public void ResetOccupiedSpots()
    {
        for (int i=0; i < OccupiedSpot.Length; i++)
        {
            OccupiedSpot[i] = false;
        }
    }

}
