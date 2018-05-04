using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personel : Character {

    //public GameController Controller;
    private Vector3 Target;
    private GameObject TargetObject;
    private GameObject[] WaypointRecord;
    public IPersJob Job;

    // Use this for initialization
    void Start () {
        Debug.Log("Personel Start");
        TargetObject = this.gameObject;
        Job = new Idle();
        StartCoroutine("DoTheJob");
    }
	
	// Update is called once per frame
	void Update () {
        MoveTo(Target);
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

    private IEnumerator DoTheJob()
    {
        while (true)
        {
            bool isReachedTarget;
            if (TargetObject == this.gameObject)
            {
                isReachedTarget = false;
            }
            else
            {
                isReachedTarget = TargetReached(SetTargetVector(TargetObject));
            }
            TargetObject = Job.JobInstructions(isReachedTarget);
            if (TargetObject == null)
            {
                TargetObject = this.gameObject;
            }
            Target = SetTargetVector(TargetObject);            
            yield return new WaitForSeconds(1);
        }
    }
}
