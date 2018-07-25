using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : Room {

    [SerializeField]
    private GameObject[] Seats;
    public List<GameObject> AvailableSeats;
    
    public List<Customer> custAtBar = new List<Customer>();

    private void Awake()
    {
        AvailableSeats.Clear();
        foreach(GameObject seat in Seats)
        {
            AvailableSeats.Add(seat);
        }
    }

    public int SeatIndex(GameObject seat)
    {
        return System.Array.IndexOf(Seats, seat);
    }

    public GameObject FindSeat(int seatIndex)
    {
        return Seats[seatIndex];
    }
}
