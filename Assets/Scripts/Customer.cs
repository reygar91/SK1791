//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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
    public int prototypeID;
    public Outfit outfit;
    //delegate bool TaskFuncDelegate(Vector3 target);
    //TaskFuncDelegate TaskCompleted;

    private ICustBehaviour Behaviour;

    private void Awake()
    {
        AnimatorComponent = GetComponentInChildren<Animator>();
        reception = FindObjectOfType<Reception>();
        //GameController.Instance.CharList.Add(this);
        outfit = GetComponentInChildren<Outfit>();
    }

    private void OnEnable()
    {
        Prototype(prototypeID);
    }
    private void OnDisable()
    {
        prototypeID = 0; //Debug.Log("CustDisabled");
        AnimatorComponent.enabled = false;
    }


    private void Update()
    {
        if (!TimeFlow.Instance.isPause)
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
            if (TimeFlow.Instance.isPause)
            {
                yield return new WaitWhile(() => TimeFlow.Instance.isPause);
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

    public void Prototype(int prototypeID)
    {
        switch (prototypeID)
        {
            case 0:
                TargetVector = reception.EntrancePoint.transform.position;
                Behaviour = null;
                Patience = 25;
                Wait = false;
                int RandomNumber = UnityEngine.Random.Range(0, 1000);
                name = "Customer_" + RandomNumber;
                AnimatorComponent.enabled = true;
                StartCoroutine("CountDown");
                StartCoroutine("Relax");

                outfit.SetOutfit();
                break;
            case 1:
                break;
        }
    }

}
