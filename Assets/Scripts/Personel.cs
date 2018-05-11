using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personel : Character {

    //public GameController Controller;
    private Vector3 TargetVector;
    private GameObject[] WaypointRecord;
    public IPersJob Job;
    private Animator AnimatorComponent;

    private void Awake()
    {
        AnimatorComponent = GetComponentInChildren<Animator>();
    }

    // Use this for initialization
    void Start () {
        Job = new Idle();
        //StartCoroutine("DoTheJob");
    }
	
	// Update is called once per frame
	void Update () {
        float delta = MoveTo(TargetVector);
        if (delta != 0 && AnimatorComponent.GetInteger("AnimationID") != 1)
        {
            AnimatorComponent.SetInteger("AnimationID", 1);
        }
    }
    
    public void SelectJob(string JobName)
    {
        switch (JobName)
        {
            case "Waiter":
                Job = new Waiter();                
                break;
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
