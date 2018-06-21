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
    //public Animator AnimatorComponent
    //{
    //    get;    private set;
    //}


    public bool Wait;
    public float AnimationTime;
    //delegate bool TaskFuncDelegate(Vector3 target);
    //TaskFuncDelegate TaskCompleted;

    private ICustBehaviour Behaviour;

    private void Awake()
    {
        AnimatorComponent = GetComponentInChildren<Animator>();
        reception = FindObjectOfType<Reception>();
        GameController.CharList.Add(this);
    }

    private void OnEnable()
    {
        TargetVector = reception.EntrancePoint.transform.position;
        Behaviour = null;
        Patience = 25;
        Wait = false;
        int RandomNumber = UnityEngine.Random.Range(0, 1000);
        name = "Customer_" + RandomNumber;
        AnimatorComponent.enabled = true;
        StartCoroutine("CountDown");
        StartCoroutine("Relax");
    }
    private void OnDisable()
    {
        AnimatorComponent.enabled = false;
    }


    private void Update()
    {
        if (!TimeFlow.isPause)
        {
            MoveTo(TargetVector);
        }           
    }

    private IEnumerator Relax()
    {
        while (true)
        {
            if (Wait)
            {
                Wait = false;
                yield return new WaitForSeconds(AnimationTime);
            } else
            {
                AnimatorComponent.SetInteger("AnimationID", 0);
                if (Patience <= 0)
                {
                    TargetVector = Behaviour.LeaveRoom();
                }
                else if (Behaviour != null)
                {
                    TargetVector = Behaviour.RoomBehaviour();
                }
                yield return new WaitUntil(() => hasReachedTarget(TargetVector));
            }  
        }
    }
    private IEnumerator CountDown()
    {
        while (true)
        {
            if (TimeFlow.isPause)
            {
                yield return new WaitWhile(() => TimeFlow.isPause);
            }
            else
            {
                Patience--;
                if (Patience <= 0 && Wait)
                {
                    Wait = false;
                }
                yield return new WaitForSeconds(1);
            }
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
                    Behaviour.SwitchRoom(); //Debug.Log("leave room onTirggerEnter switch");
                }
                Behaviour = new CustReception(this, reception);
                TargetVector = transform.position; //make itself a target so hasReachedTarged evaluates to true
            }
            if (another.name.Contains("Bar"))
            {
                Behaviour.SwitchRoom();
                Bar RoomType = another.GetComponent<Bar>();
                Behaviour = new CustBar(this, RoomType);
                TargetVector = transform.position; //make itself a target so hasReachedTarged evaluates to true

            }            
        }        
    }

}
