using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : Character {

    public int Patience
    {
        get;
        private set;        
    }
    public GameObject TargetObject
    {
        get;
        private set;
    }

    private Animator AnimatorComponent;
    private ICustBehaviour Behaviour;
    private Reception reception;

    private void Awake()
    {
        AnimatorComponent = GetComponentInChildren<Animator>();
        reception = FindObjectOfType<Reception>();
    }

    private void OnEnable()
    {        
        TargetObject = reception.EntrancePoint;
        Patience = 15;
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
            Patience--;
            if (Patience <= 0)
            {
                TargetObject = Behaviour.LeaveRoom();
            } else if (Behaviour != null)
            {
                TargetObject = Behaviour.RoomBehaviour();
            }                
            yield return new WaitForSeconds(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Room")
        {
            if (other.name.Contains("Reception"))
            {
                Behaviour = new CustReception(this, reception, AnimatorComponent);
                //Behaviour.RoomBehaviour(this, reception, AnimatorComponent);
            }
            if (other.name.Contains("Bar"))
            {
                Behaviour.SwitchRoom();
                Bar RoomType = other.GetComponentInChildren<Bar>(); //Debug.Log("BarTime");
                Behaviour = new CustBar(this, RoomType, AnimatorComponent);
            }            
        }        
    }


}
