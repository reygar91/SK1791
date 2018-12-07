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

    public override Transform CheckActivityWithinRoom()
    {
        Transform result = null;
        List<Transform> Seats = new List<Transform>();
        foreach (Transform seat in CustomerInteractionObjects)
        {
            if (BarMNGR.Instance.AvailableSeats.Contains(seat))
                Seats.Add(seat);
        }
        if (Seats.Count != 0)
        {
            int index = Random.Range(0, Seats.Count);
            result = Seats[index];
        }
        

        return result;
    }

    //public override void SetActivityToSelectedPersonnel()
    //{
    //    //Debug.Log(SelectorForPersonnel.SelectedPersonnel.name);
    //    SelectorForPersonnel.SelectedPersonnel.Focus.Activity = this;
    //    base.SetActivityToSelectedPersonnel();
    //}

    }
