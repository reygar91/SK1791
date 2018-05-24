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
    
    delegate bool TaskFuncDelegate(Vector3 target);
    TaskFuncDelegate TaskCompleted;

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
        Patience = 25;
        StartCoroutine("CountDown");
        StartCoroutine("Relax");
    }

    private void OnDisable()
    {
        //AnimatorComponent.StopPlayback();
    }

    private void Update()
    {
        float delta = MoveTo(TargetVector);      
        if (delta != 0 && AnimatorComponent.GetInteger("AnimationID") != 1)
        {
            AnimatorComponent.SetInteger("AnimationID", 1); 
        }      
    }
    /*
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
            //yield return new WaitWhile();
        }
    }
    */
    private IEnumerator Relax()
    {
        while (true)
        {
            AnimatorComponent.SetInteger("AnimationID", 0);
            TaskCompleted = hasReachedTarget;
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
            //yield return new WaitForSeconds(.5f);
            yield return new WaitUntil(() => TaskCompleted(TargetVector));
        }
    }
    private IEnumerator CountDown()
    {
        while (true)
        {
            Patience--;
            if (Patience <= 0)
            {
                //TargetVector = Behaviour.LeaveRoom();
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject another = other.transform.parent.gameObject;
        if (other.tag == "Room")
        {
            if (another.name.Contains("Reception"))
            {
                if (Behaviour != null)
                {
                    Behaviour.SwitchRoom();
                }
                Behaviour = new CustReception(this, reception);
                TaskCompleted = AlwaysTrue;
                //Behaviour.RoomBehaviour(this, reception, AnimatorComponent);
            }
            if (another.name.Contains("Bar"))
            {
                Behaviour.SwitchRoom();
                Bar RoomType = another.GetComponent<Bar>();
                //Bar RoomType = other.GetComponentInChildren<Bar>(); //Debug.Log("BarTime");
                Behaviour = new CustBar(this, RoomType);
                TaskCompleted = AlwaysTrue;
            }            
        }        
    }

    private bool AlwaysTrue(Vector3 anyVector)
    {
        return true;
    }


}
