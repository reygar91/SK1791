using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : Character {

    private int patience;
    public GameController Controller;
    //public ICustBehavior CustBehavior;
    private Vector3 Target;
    private int StatusID;
    private GameObject[] WaypointRecord;
    //public List<Customer> CustomersAtBar = new List<Customer>();

    private SeatManager SeatOccupied;

    private void OnEnable()
    {
        //CustBehavior = new EnterTheBar();
        WaypointRecord = new GameObject[1];
        WaypointRecord[0] = Controller.WayPoint[0].gameObject;
        StatusID = 0;
        patience = 15;
        StartCoroutine("Relax");
    }
    

    private void Update()
    {
        MoveTo(Target);        
    }

    private IEnumerator Relax()
    {
        while (true)
        {
            patience--;
            //Debug.Log("StatusId: " + StatusID);
            if (patience <= 0)
                StatusID = 100;
                
            switch (StatusID)
            {
                case 0: //just spawned
                    Target = SetTargetVector(Controller.WayPoint[1].gameObject);
                    StatusID = 1;                    
                    break;
                case 1: //On the way to Bar Entrance
                    if (TargetReached(Target))
                    {
                        Target = SetTargetVector(FindASeat(SeatManager.Seats));
                        AddWaypoint(Controller.WayPoint[1]);
                    }                        
                    break;
                case 2: //At the Entrance, but no free seats available
                    Target = SetTargetVector(FindASeat(SeatManager.Seats));
                    break;
                case 3: //Succesfully finded a seat, on the way to it
                    if (TargetReached(Target))
                    {
                        Waiter.CustomersAtBar.Add(this);
                        StatusID = 4;                        
                        //Debug.Log(this + "added; new Count is " + Waiter.CustomersAtBar.Count);
                        //Debug.Log("DrinkingABeer");
                    }
                    break;
                case 4: //On the seat, waiting for waiter                    
                    if (patience < 2) //temporary measure for making seat free again
                    {
                        //Debug.Log(this + "removed; new Count is " + Waiter.CustomersAtBar.Count);
                        Waiter.CustomersAtBar.Remove(this);
                        SeatOccupied.Occupied = false;                     
                    }
                    break;
                case 100: //run out of patience
                    LeaveTheBar();
                    break;
            }
            yield return new WaitForSeconds(1);
        }
    }

    private GameObject FindASeat(SeatManager[] Seats)
    {
        bool[] SeatsOccupancy = new bool[Seats.Length];
        for (int i =0; i < Seats.Length; i++)
        {            
            SeatsOccupancy[i] = Seats[i].Occupied;
        }
        GameObject iTarget;        
        Array.Sort(SeatsOccupancy, Seats);
        int index = Array.LastIndexOf(SeatsOccupancy, false);
        //Debug.Log("LastIndexOf false: " + index);
        if (index == -1)
        {
            StatusID = 2;
            //Debug.Log("No free Seats");
            return this.gameObject;
        }
        else
        {
            int chosenIndex = UnityEngine.Random.Range(0, index);
            //int chosenIndex = UnityEngine.Random.Range(index, Seats.Length);
            iTarget = Seats[chosenIndex].gameObject;
            SeatOccupied = Seats[chosenIndex];
            Seats[chosenIndex].Occupied = true;            
            StatusID = 3;
            return iTarget;
        }
    }

    private void AddWaypoint(Transform t)
    {
        Array.Resize(ref WaypointRecord, WaypointRecord.Length + 1);
        WaypointRecord[WaypointRecord.Length - 1] = t.gameObject;
    }

    private void LeaveTheBar()
    {
        Target = SetTargetVector(WaypointRecord[WaypointRecord.Length - 1]);
        if (TargetReached(Target))
        {
            if (WaypointRecord.Length == 1)
            {
                gameObject.SetActive(false);
            } else
            {
                Array.Resize(ref WaypointRecord, WaypointRecord.Length - 1);
                Target = SetTargetVector(WaypointRecord[WaypointRecord.Length - 1]);
            }
        }
    }
    
    public int getPatience()
    {
        return patience;
    }
}
