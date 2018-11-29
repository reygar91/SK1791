using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : Activity {
    

    public int SeatIndex(GameObject seat)
    {
        return System.Array.IndexOf(CustomerInteractionObjects, seat);
    }

    public Transform FindSeat(int seatIndex)
    {
        return CustomerInteractionObjects[seatIndex];
    }

    private void RegisterSeats()
    {
        foreach (Transform seat in CustomerInteractionObjects)
        {
            BarMNGR.Instance.AvailableSeats.Add(seat);
        }
    }

    public override void RegisterInteractionObjects()
    {
        RegisterSeats();
    }

    public override void Load(int ObjectIndex, myCharacterController CC)
    {
        BarMNGR.Instance.AvailableSeats.Remove(CustomerInteractionObjects[ObjectIndex]);
    }
}
