using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : Character {

    private int patience;
    private GameController Controller;
    private GameObject TargetObject;
    private int StatusID;
    public GameObject[] WaypointRecord;

    private GameObject Seat;
    private Animator AnimatorComponent;

    public ICustBehaviour Behaviour;

    private void Awake()
    {
        AnimatorComponent = GetComponentInChildren<Animator>();
        Controller = FindObjectOfType<GameController>();
    }

    private void OnEnable()
    {
        WaypointRecord = new GameObject[1];
        WaypointRecord[0] = Controller.WayPoint[0].gameObject;
        StatusID = 0;
        patience = 15;
        StartCoroutine("Relax");        
    }    

    private void Update()
    {
        float delta = MoveTo(TargetObject);      
        if (delta != 0 && AnimatorComponent.GetInteger("AnimationID") != 1)
        {
            AnimatorComponent.SetInteger("AnimationID", 1); 
        }      
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
                    TargetObject = Controller.WayPoint[1].gameObject;
                    StatusID = 1;                    
                    break;
                case 1: //On the way to Bar Entrance
                    if (hasReachedTarget(TargetObject))
                    {
                        AnimatorComponent.SetInteger("AnimationID", 0);
                        TargetObject = FindASeat();
                        AddWaypoint(Controller.WayPoint[1]);
                    }                        
                    break;
                case 2: //At the Entrance, but no free seats available
                    TargetObject = FindASeat();
                    break;
                case 3: //Succesfully finded a seat, on the way to it
                    if (hasReachedTarget(TargetObject))
                    {
                        AnimatorComponent.SetInteger("AnimationID", 0);//change to sitting animation
                        Waiter.CustomersAtBar.Add(this);
                        StatusID = 4;                        
                    }
                    break;
                case 4: //On the seat, waiting for waiter                    
                    if (patience < 2) //temporary measure for making seat free again
                    {
                        Waiter.CustomersAtBar.Remove(this); //removing from list of custs waited to be served
                        Controller.unOccupiedSeats.Add(Seat); //adding seat back to list of unOccupied seats               
                    }
                    break;
                case 100: //run out of patience
                    Behaviour.SwitchRoom();
                    Behaviour = new CustLeave();
                    Behaviour.RoomBehaviour(this, this, AnimatorComponent);
                    Behaviour.RoomBehaviour();
                    break;
            }
            yield return new WaitForSeconds(1);
        }
    }

    private GameObject FindASeat()
    {
        if (Controller.unOccupiedSeats.Count != 0)
        {
            int RandomSeat = UnityEngine.Random.Range(0, Controller.unOccupiedSeats.Count);
            Seat = Controller.unOccupiedSeats[RandomSeat];
            Controller.unOccupiedSeats.Remove(Seat);
            StatusID = 3;
            return Seat;
        }
        else
        {
            StatusID = 2;
            return null;
        }
    }

    private void AddWaypoint(Transform t)
    {
        Array.Resize(ref WaypointRecord, WaypointRecord.Length + 1);
        WaypointRecord[WaypointRecord.Length - 1] = t.gameObject;
    }
    
    public int getPatience()
    {
        return patience;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Room")
        {
            if (other.name.Contains("Reception"))
            {
                Reception RoomType = other.GetComponent<Reception>();
                Behaviour = new CustReception();
                Behaviour.RoomBehaviour(this, RoomType, AnimatorComponent);
                StartCoroutine("RoomBehaviour");
            }
            if (other.name.Contains("Bar"))
            {
                Debug.Log("CustEnteredBar");
            }            
        }        
    }

    private IEnumerator RoomBehaviour()
    {
        while (true)
        {
            TargetObject = Behaviour.RoomBehaviour();
            yield return new WaitForSeconds(.5f);
        }
    }

}
