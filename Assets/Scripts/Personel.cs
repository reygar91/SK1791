using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personel : Character {

    public Vector3 TargetVector
    {
        get; private set;
    }
    public Reception reception
    {
        get; private set;
    }
    public Animator AnimatorComponent
    {
        get; private set;
    }

    private IPersBehaviour Behaviour;

    private void Awake()
    {
        AnimatorComponent = GetComponentInChildren<Animator>();
    }

    // Use this for initialization
    void Start () {
        //Job = new Idle();
        StartCoroutine("Behave");
    }
	
	// Update is called once per frame
	void Update () {
        float delta = MoveTo(TargetVector);
        if (delta != 0 && AnimatorComponent.GetInteger("AnimationID") != 1)
        {
            AnimatorComponent.SetInteger("AnimationID", 1);
        }
    }

    private IEnumerator Behave()
    {
        while (true)
        {
            if (Behaviour != null)
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
    /*
    public void SelectJob(string JobName)
    {
        switch (JobName)
        {
            case "Waiter":
                //Job = new Waiter();                
                break;
        }
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Room")
        {
            if (other.name.Contains("Bar"))
            {
                //Behaviour.SwitchRoom();
                Bar RoomType = other.GetComponentInChildren<Bar>(); Debug.Log("BarTime");
                Behaviour = new PersBar(this, RoomType);
            }
        }
    }

    /*
    private IEnumerator DoTheJob()
    {
        while (true)
        {
            bool isReachedTarget;
            if (TargetObject == gameObject)
            {
                isReachedTarget = false;
            }
            else
            {
                isReachedTarget = hasReachedTarget(TargetVector);
                if (isReachedTarget) { AnimatorComponent.SetInteger("AnimationID", 0); }
            }
            TargetObject = Job.JobInstructions(isReachedTarget);
            yield return new WaitForSeconds(1);
        }
    }
    */
}
