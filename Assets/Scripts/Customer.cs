using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : Character {

    public int Patience
    {
        get;    private set;        
    }
    public Vector3 TargetVector
    {
        get;    private set;
    }
    public Reception reception
    {
        get;    private set;
    }
    public Animator AnimatorComponent
    {
        get;    private set;
    }

    private ICustBehaviour Behaviour;

    private void Awake()
    {
        AnimatorComponent = GetComponentInChildren<Animator>();
        reception = FindObjectOfType<Reception>();
    }

    private void OnEnable()
    {
        TargetVector = reception.EntrancePoint.transform.position;
        Behaviour = null;
        Patience = 45;
        StartCoroutine("Relax");
    }

    private void OnDisable()
    {
        AnimatorComponent.StopPlayback();
    }

    private void Update()
    {
        float delta = MoveTo(TargetVector);      
        if (delta != 0 && AnimatorComponent.GetInteger("AnimationID") != 1)
        {
            AnimatorComponent.SetInteger("AnimationID", 1); 
        }      
    }

    private IEnumerator Relax()
    {
        while (true)
        {
            Patience--;
            if (Patience <= 0)
            {                
                TargetVector = Behaviour.LeaveRoom();
            }
            else if (Behaviour != null)
            {
                TargetVector = Behaviour.RoomBehaviour(); //Debug.Log(transform.position.z);
            }
            if (hasReachedTarget(TargetVector))
            {
                AnimatorComponent.SetInteger("AnimationID", 0);
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Room")
        {
            if (other.name.Contains("Reception"))
            {
                if (Behaviour != null)
                {
                    Behaviour.SwitchRoom();
                }
                Behaviour = new CustReception(this, reception);
                //Behaviour.RoomBehaviour(this, reception, AnimatorComponent);
            }
            if (other.name.Contains("Bar"))
            {
                Behaviour.SwitchRoom();
                Bar RoomType = other.GetComponentInChildren<Bar>(); //Debug.Log("BarTime");
                Behaviour = new CustBar(this, RoomType);
            }            
        }        
    }


}
