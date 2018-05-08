using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : Character {

    private int patience;
    //private GameController Controller;
    private GameObject TargetObject;
    private int StatusID;
    //public GameObject[] WaypointRecord;

    //private GameObject Seat;
    private Animator AnimatorComponent;

    public ICustBehaviour Behaviour;

    private void Awake()
    {
        AnimatorComponent = GetComponentInChildren<Animator>();
        //Controller = FindObjectOfType<GameController>();
    }

    private void OnEnable()
    {
        TargetObject = GameObject.FindGameObjectWithTag("Finish");//temporalily
        //WaypointRecord = new GameObject[1];
        //WaypointRecord[0] = Controller.WayPoint[0].gameObject;
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
                    //TargetObject = Controller.WayPoint[1].gameObject;
                    StatusID = 1;                    
                    break;
                case 1: //On the way to Bar Entrance
                    if (hasReachedTarget(TargetObject))
                    {
                        AnimatorComponent.SetInteger("AnimationID", 0);
                        //TargetObject = FindASeat();
                        //AddWaypoint(Controller.WayPoint[1]);
                    }                        
                    break;
                case 100: //run out of patience
                    //Behaviour.SwitchRoom();//should make it run only once
                    StopCoroutine("RoomBehaviour");
                    TargetObject = Behaviour.LeaveRoom(); //Debug.Log(TargetObject.name);
                    break;
            }
            yield return new WaitForSeconds(1);
        }
    }
    /*
    private void AddWaypoint(Transform t)
    {
        Array.Resize(ref WaypointRecord, WaypointRecord.Length + 1);
        WaypointRecord[WaypointRecord.Length - 1] = t.gameObject;
    }
    */
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
                Behaviour.SwitchRoom();
                Bar RoomType = other.GetComponentInChildren<Bar>(); //Debug.Log("BarTime");
                Behaviour = new CustBar();
                Behaviour.RoomBehaviour(this, RoomType, AnimatorComponent);
                StartCoroutine("RoomBehaviour");
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
