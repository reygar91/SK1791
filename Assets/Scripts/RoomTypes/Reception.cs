﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reception : Room {

    public GameObject SpawnPoint, EntrancePoint, InternalDoor;

    public GameObject[] WaitInLinePoints;
    public bool[] OccupiedSpot;


    private void Awake()
    {
        roomsList.Add(this); // later this should be removed, cuz reception will be added after every load
    }
}
