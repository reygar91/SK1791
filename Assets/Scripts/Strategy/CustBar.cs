using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustBar : ICustBehaviour
{
    Customer cust;
    Bar bar;
    Animator animator;
    //GameObject Target;
    GameObject Seat;    

    public void RoomBehaviour<T>(Customer customer, T RoomType, Animator AnimatorComponent)
    {
        cust = customer;
        bar = RoomType as Bar;
        animator = AnimatorComponent;
    }

    public GameObject RoomBehaviour()
    {
        if (bar.Seats.Count != 0 && !Seat)
        {
            int RandomSeat = UnityEngine.Random.Range(0, bar.Seats.Count);
            Seat = bar.Seats[RandomSeat];
            bar.Seats.Remove(Seat);
        }
        return Seat;
    }

    public void SwitchRoom()
    {
        bar.Seats.Add(Seat);
    }

    public GameObject LeaveRoom()
    {
        return bar.Doors;
    }
}
