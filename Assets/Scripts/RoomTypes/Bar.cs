using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : Room {

    public List<GameObject> Seats;
    //
    public Transform[] SeatsPositions;
    public bool[] SeatsAvailability;
    public List<Customer> custAtBar = new List<Customer>();

}
